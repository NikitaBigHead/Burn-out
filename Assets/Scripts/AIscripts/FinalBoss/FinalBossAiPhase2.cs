using System.Collections;
using UnityEngine;

public class FinalBossAiPhase2 : MonoBehaviour
{
    private enum State
    {
        PrepareToRavenAttack,
        RavenAttack,
        PrepareToRun,
        Running,
        RunningWithAcceleration,
        Waiting,
        PrepareToWaveAttack,
        ContinueWaveAttack,
        WaveAttack,
        Death
    }

    private State state = State.Waiting;

    // Концовка
    [SerializeField]
    private EndingController ending;

    // Вороны влетают в чучело во время вступления
    [SerializeField]
    private Transform ravens;

    [SerializeField]
    private GameObject ravensPrefab;

    public float ravensDefaultOffset = -2.24f;

    //=========================================================================
    //=========================================================================
    //=========================================================================

    // Атака волной

    [SerializeField] /* Префаб волны */
    private GameObject wavePrefab;

    // Переменные настройки атаки

    public float waveDamage = 5f;           /* Урон при касании с волной */
    public float waveThickness = 0.65f;     /* Толщина волны */
    public float waveSafetyOffset = 0.75f;  /* Расстояние до безопасной внутренней зоны */
    public float waveSpeed = 0.85f;         /* Скорость распространения волны */
    public float waveMaxRange = 20f;        /* Максимальная дальность распространения */

    public float minWaveDelay = 0.8f;       /* Минимальная задержка до спавна новой волны */
    public float maxWaveDelay = 2.1f;       /* Максимальная задержка до спавна новой волны */

    public int minWavesPerWaveAttack = 3;   /* Минимум волн за атаку */
    public int maxWavesPerWaveAttack = 6;   /* Максимум волн за атаку */

    // Локальные переменные

    private float currentWaveTime = 0;
    private float maxWaveTime = 0;

    private int currentWaveCount = 0;
    private int maxWaveCount = 0;

    //=========================================================================
    //=========================================================================
    //=========================================================================

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, -1.5f, 0);

    [SerializeField]
    private Vector3 targetOffset = new Vector3(0, -0.295f, 0);

    // Бег в сторону игрока

    public float delayAfterRunning = 0.5f; /* Задержка перед следующей атакой после бега */
    public float delayAfterMaxSpeed = 1f; /* Время бега после достижения максимаьной скорости */

    public float delayFollowUpdate = 0.2f; /* Задержка перед изменением курса в сторону игрока */

    public float acceleration = 0.001f; /* Ускорение */
    public float maxSpeed = 0.112f; /* Максимальная развиваемая скорость */ 
    public float startSpeed = 0f; /* Скорость при начале движения */

    public float damage = 20f; /* Урон при касании */

    // Локальные переменные

    private float currentSpeed = 0f;
    private Vector2 direction;

    //=========================================================================
    //=========================================================================
    //=========================================================================

    private Animator animator;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        StartCoroutine(ResetStateAfterDelay(State.PrepareToRavenAttack, 1f));
    }

    void SelectAnimationBasedOnDirection()
    {
        Vector2 directionForAnimation = Quaternion.AngleAxis(45f, Vector3.forward) * direction;
        if (directionForAnimation.y > 0)
            if (directionForAnimation.x > 0) animator.Play("RightAnimation");
            else animator.Play("UpAnimation");
        else
            if (directionForAnimation.x > 0) animator.Play("DownAnimation");
        else animator.Play("LeftAnimation");
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.PrepareToRun:
                currentSpeed = startSpeed;
                state = State.RunningWithAcceleration;
                StartCoroutine(FollowPlayer());
                break;

            case State.Running:
                
                transform.position += new Vector3(direction.x, direction.y) * currentSpeed;
                break;

            case State.RunningWithAcceleration:
                currentSpeed += acceleration;
                transform.position += new Vector3(direction.x, direction.y) * currentSpeed;
                if (currentSpeed >= maxSpeed)
                {
                    StartCoroutine(WaitForResetAfterDelay(State.PrepareToWaveAttack, delayAfterMaxSpeed, delayAfterRunning));
                    state = State.Running;
                }
                break;

            case State.PrepareToWaveAttack:
                maxWaveTime = Random.Range(minWaveDelay, maxWaveDelay);
                currentWaveTime = maxWaveTime;
                currentWaveCount = 0;
                maxWaveCount = Random.Range(minWavesPerWaveAttack, maxWavesPerWaveAttack + 1);
                state = State.WaveAttack;
                break;

            case State.ContinueWaveAttack:
                currentWaveTime = 0;
                maxWaveTime = Random.Range(minWaveDelay, maxWaveDelay);
                state = State.WaveAttack;
                if (currentWaveCount >= maxWaveCount)
                {
                    state = State.PrepareToRun;
                }
                break;

            case State.WaveAttack:
                currentWaveTime += Time.deltaTime;
                if (currentWaveTime > maxWaveTime)
                {
                    currentWaveCount++;
                    state = State.ContinueWaveAttack;
                    GameObject wave = Instantiate(wavePrefab);
                    wave.transform.position = transform.position;
                    wave.GetComponent<CircleWave>().Launch(waveDamage, waveThickness, waveSafetyOffset, waveSpeed, waveMaxRange);
                }
                break;

            case State.PrepareToRavenAttack:
                state = State.RavenAttack;
                ravens.transform.position = transform.position + new Vector3(0, ravensDefaultOffset, 0);
                ravens.GetComponent<Animator>().Play("InAnimation");
                break;

            case State.RavenAttack:
                StartCoroutine(ResetStateAfterDelay(State.PrepareToRun, 1f));
                state = State.Waiting;
                break;

            case State.Waiting:
                animator.Play("IdleAnimation");
                break;

            case State.Death:
                break;
        }
    }

    public void OnDeath(GameObject sender)
    {
        state = State.Death;
        ending.EndGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<AttackableEntity>().RecieveDamage(damage);
        }
    }

    private IEnumerator ResetStateAfterDelay(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        state = newState;
    }

    private IEnumerator WaitForResetAfterDelay(State newState, float delay, float waitingDelay)
    {
        yield return new WaitForSeconds(delay);
        state = State.Waiting;
        yield return new WaitForSeconds(waitingDelay);
        state = newState;
    }

    private IEnumerator FollowPlayer()
    {
        while (state == State.Running || state == State.RunningWithAcceleration)
        {
            direction = new Vector2(target.position.x + targetOffset.x - transform.position.x - offset.x, target.position.y + targetOffset.y - transform.position.y - offset.y).normalized;
            SelectAnimationBasedOnDirection();
            yield return new WaitForSeconds(delayFollowUpdate);
        }
    }
}

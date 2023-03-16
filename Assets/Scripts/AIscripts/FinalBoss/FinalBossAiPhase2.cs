using System.Collections;
using System.Collections.Generic;
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
        WaveAttack,
        Death
    }

    private State state = State.Waiting;

    [SerializeField]
    private GameObject ravensPrefab;

    //[SerializeField]
    //private Transform ravens;

    public float ravensDefaultOffset = -2.24f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, -1.5f, 0);

    [SerializeField]
    private Vector3 targetOffset = new Vector3(0, -0.295f, 0);

    public float delayAfterRunning = 0.5f;
    public float delayAfterMaxSpeed = 1f;

    public float delayFollowUpdate = 0.2f;

    public float acceleration = 0.001f;
    public float maxSpeed = 0.112f;
    public float startSpeed = 0f;

    public float damage = 20f;

    private float currentSpeed = 0f;
    private Vector2 direction;

    private Animator animator;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        //ravens.transform.position = transform.position + new Vector3(0, ravensDefaultOffset, 0);

        StartCoroutine(ResetStateAfterDelay(State.PrepareToRun, 1f));
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
                //currentSpeed;
                //direction = new Vector2(target.position.x + targetOffset.x - transform.position.x - offset.x, target.position.y + targetOffset.y - transform.position.y - offset.y).normalized;
                //SelectAnimationBasedOnDirection();
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
                                                         //State.PrepareToRun
                    StartCoroutine(WaitForResetAfterDelay(State.PrepareToRavenAttack, delayAfterMaxSpeed, delayAfterRunning));
                    state = State.Running;
                }
                break;

            case State.PrepareToWaveAttack:

                break;

            case State.WaveAttack:
                break;

            case State.PrepareToRavenAttack:
                state = State.RavenAttack;
                Debug.Log("preparing to raven attack");
                GameObject spawnedRavens = Instantiate(ravensPrefab);
                RavensAi ravensAi = spawnedRavens.GetComponent<RavensAi>();
                if (ravensAi == null)
                {
                    Debug.Log("RAVENAI == NULL");
                }
                ravensAi.Ensnare(transform, target.position, ravensDefaultOffset, 0.1f);

                //this.ravens.transform.position = transform.position + new Vector3(0, ravensDefaultOffset, 0);
                //this.ravens.GetComponent<Animator>().Play("OutAnimation");
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

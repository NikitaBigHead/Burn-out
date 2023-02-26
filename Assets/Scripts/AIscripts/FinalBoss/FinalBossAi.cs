using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinalBossAi : MonoBehaviour
{
    private enum State
    {
        Running,
        Waiting,
        PrepareToRun,
        PrepareToCenter
    }

    private State state = State.Waiting;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, -1.5f, 0);

    [SerializeField]
    private Vector3 targetOffset = new Vector3(0, -0.295f, 0);

    public float delayAfterHit = 1f;
    public float backToCenter = 0.5f;

    public float acceleration = 0.001f;
    public float startSpeed = 0f;

    public float damage = 20f;

    private float currentSpeed = 0f;
    private Vector2 direction;

    private Animator animator;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        state = State.PrepareToRun;
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
        switch (state) {
            case State.PrepareToRun:
                //currentSpeed;
                direction = new Vector2(target.position.x + targetOffset.x - transform.position.x - offset.x, target.position.y + targetOffset.y - transform.position.y - offset.y).normalized;
                SelectAnimationBasedOnDirection();
                state = State.Running;
                break;

            case State.Running:
                currentSpeed += acceleration;
                transform.position += new Vector3(direction.x, direction.y) * currentSpeed;
                break;

            case State.PrepareToCenter:
                currentSpeed = startSpeed;
                direction = new Vector2(-transform.position.x - offset.x, -transform.position.y - offset.y).normalized;
                SelectAnimationBasedOnDirection();
                state = State.Running;
                break;

            case State.Waiting:
                break;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.Running)
        {
            BarnWall wall = collision.gameObject.GetComponent<BarnWall>();

            if (wall != null)
            {
                state = State.Waiting;
                wall.ReceiveDamage(0);
                animator.Play("HitAnimation");
                StartCoroutine(ResetRun());
            }
            else if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<AttackableEntity>().RecieveDamage(damage);
            }
        }
    }

    private IEnumerator ResetRun()
    {
        yield return new WaitForSeconds(delayAfterHit);
        state = State.PrepareToCenter;
        yield return new WaitForSeconds(backToCenter);
        state = State.PrepareToRun;
    }
}

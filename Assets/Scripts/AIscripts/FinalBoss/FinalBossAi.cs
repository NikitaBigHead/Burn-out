using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinalBossAi : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public float delayAfterHit = 1f;

    public float acceleration = 0.001f;
    public float startSpeed = 0f;

    public float damage = 20f;

    private float currentSpeed = 0f;
    private Vector2 direction;

    private bool canRun = true;
    private bool running = false;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void FixedUpdate()
    {
        if (canRun)
        {
            currentSpeed = startSpeed;
            direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
            canRun = false;
            running = true;
        }
        else if (running)
        {
            currentSpeed += acceleration;
            this.transform.position += new Vector3(direction.x, direction.y, 0) * currentSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BarnWall wall = collision.gameObject.GetComponent<BarnWall>();
        
        if (wall != null)
        {
            wall.ReceiveDamage(0);
            running = false;
            Invoke("ResetRun", delayAfterHit);
        } else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<AttackableEntity>().RecieveDamage(damage);
        }

    }

    private void ResetRun()
    {
        canRun = true;
    }
}

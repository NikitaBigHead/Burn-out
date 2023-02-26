using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangerAi : MonoBehaviour
{
    [SerializeField]
    private SimpleProjectile projectile;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 spawnOffset = Vector3.zero;

    public float throwDelay = 1f;

    public float projectileSize = 0.4f;
    public float projectileSpeed = 0.2f;
    public float projectileRange = 10f;

    public float scaryDistance = 3f;
    
    private NavMeshAgent agent;

    private bool canThrow = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    void Update()
    {
        Vector2 diff = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        if (diff.magnitude < projectileRange)
        {
            if (diff.magnitude > scaryDistance)
            {
                agent.SetDestination(transform.position);
            }
            else
            {
                agent.SetDestination(new Vector3(transform.position.x - diff.x, transform.position.x - diff.y));
            }
            if (canThrow)
            {
                Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
                Instantiate(projectile, transform.position + spawnOffset, Quaternion.identity).Launch(projectileSpeed, projectileSize, projectileRange, direction);

                canThrow = false;
                Invoke("ResetThrow", throwDelay);
            }
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    void ResetThrow()
    {
        canThrow = true;
    }
}

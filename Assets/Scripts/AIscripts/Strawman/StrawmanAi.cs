using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StrawmanAi : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private NavMeshAgent agent;
    private WeaponFork fork;

    public float attackDistance = 2f;

    private bool canAttack = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        fork = GetComponentInChildren<WeaponFork>();
    }

    private void Update()
    {
#if DEBUG
        if (target != null)
            Debug.DrawLine(transform.position, target.position, Color.red);
#endif


        agent.SetDestination(target.position);
        Vector2 diff = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        if (diff.magnitude < attackDistance && canAttack)
        {
            canAttack = false;
            Invoke("ResetAttack", fork.PerformAttack(diff.normalized));
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}

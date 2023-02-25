using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleTargetAi : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private NavMeshAgent agent;

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

    private void Update()
    {
#if DEBUG
        if (target != null)
            Debug.DrawLine(transform.position, target.position, Color.red);
#endif
        agent.SetDestination(target.position);
    }
}

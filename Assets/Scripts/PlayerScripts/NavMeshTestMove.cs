using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTestMove : MonoBehaviour
{
    Vector3 target = new Vector3(0, 0, 0);
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        agent.SetDestination(target);
    }
}

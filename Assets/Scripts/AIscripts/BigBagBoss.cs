using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBagBoss : MonoBehaviour
{
    public float bigJumpDelay = 5f;
    public float jumpDelay = 0.3f;
    public float FlightDelay = 5f;
    public float SpeedFalling = 10f;
    public float damage = 20f;
    public float jumpLength = 0.35f;

    private bool canJump = true;


    public GameObject ShadowWarning;

    private GameObject player;

    private NavMeshAgent agent;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
    private void Start()
    {
        StartCoroutine(walking());
    }

    private void resetJump()
    {
        canJump = true;
    }
    IEnumerator walking()
    {

        float start = Time.time;
        while (Time.time - start <= bigJumpDelay)
        {
            if (canJump)
            {
                canJump= false;
                Vector3 nextPos = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 1).normalized;
                nextPos = transform.position + nextPos * jumpLength;
                agent.SetDestination(nextPos);

                Invoke("resetJump", jumpDelay);
            }
           

            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(flight());

    }

    IEnumerator flight()
    {

        float start = Time.time;
        while (Time.time - start <= FlightDelay)
        {
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(walking());
    }

}

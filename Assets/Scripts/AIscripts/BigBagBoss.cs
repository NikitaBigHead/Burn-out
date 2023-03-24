using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBagBoss : MonoBehaviour
{
    public float bigJumpDelay = 5f;
    public float jumpDelay = 0.3f;
    public float FlightDelay = 5f;
    public float SpeedFalling = 20;
    public float damage = 20f;
    public float jumpLength = 0.35f;

    public float height = 5f;
    private bool canJump = true;


    public GameObject ShadowWarning;

    private GameObject player;

    private Collider2D collider;
    private NavMeshAgent agent;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        collider = GetComponent<Collider2D>();

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
                canJump = false;
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

        Vector3 position = transform.position;
        GameObject shadow = Instantiate(ShadowWarning,position,Quaternion.identity);


        Attention attention = shadow.GetComponent<Attention>();
        attention.Interval = FlightDelay;

        collider.enabled = false;
        agent.enabled = false;
        float start = Time.time;
        while (Time.time - start <= (FlightDelay/2))
        {
            //shadow.transform.position = player.transform.position;
            shadow.transform.position = Vector3.Lerp(shadow.transform.position, player.transform.position,2f* Time.deltaTime);
            position = new Vector3(position.x, position.y +=(SpeedFalling * Time.deltaTime),position.z);
            transform.position = position;
            yield return new WaitForFixedUpdate();
        }

        start = Time.time;
        position.x = shadow.transform.position.x;
        while (transform.position.y>shadow.transform.position.y)
        {
            
            position = new Vector3(position.x, position.y -= (SpeedFalling * Time.deltaTime), position.z);
            transform.position = position;
            yield return new WaitForFixedUpdate();
        }

        Collider2D[] collisions = Physics2D.OverlapCircleAll(shadow.transform.position, 0.5f);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].tag == "Player") collisions[i].gameObject.GetComponent<AttackablePlayer>().RecieveDamage(damage);
        }

        agent.enabled = true;
        collider.enabled = true;
        attention.stop();
        StartCoroutine(walking());
    }

}

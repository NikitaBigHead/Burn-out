using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public float speed = 3.0f;
    private float range = 20f;
    public float damage = 5f;
    private Vector2 direction;

    private float startPos;
    private float sign;
    public float startSpeed = 5f;

    private Transform playerCords;
    private SpriteRenderer spriteRenderer;

    public void Launch(float sign)
    {
        this.sign = sign;
        StartCoroutine(startAnim());
    }
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerCords = GameObject.FindWithTag("Player").transform;
        direction = (playerCords.position - transform.position).normalized;

        startPos = transform.position.x;
    }
    private void Start()
    {
        if (sign < 0) spriteRenderer.flipX = true;
    }

    IEnumerator startAnim()
    {
        while(sign*transform.position.x>sign*(startPos - 2.95 * sign))
        {
            float y = Mathf.Cos(transform.position.x);
            transform.position = Vector2.Lerp(transform.position, 
                new Vector2(startPos - 3 * sign, playerCords.position.y + y ), startSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(moving());

    }
    IEnumerator moving() {

        while (Mathf.Abs(playerCords.position.x - transform.position.x) >= 0.15f)
        {
            direction = (playerCords.position - transform.position).normalized;
            Vector3 translation = new Vector3(direction.x * speed , direction.y * speed, 0) * Time.deltaTime;
            transform.position += translation;
            yield return new WaitForFixedUpdate(); 
        }
        while (range >= 0)
        {
            Vector3 translation = new Vector3(direction.x * speed, direction.y * speed, 0) * Time.deltaTime;
            transform.position += translation;
            range -= translation.magnitude;
            yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
        
    }
    private void Update()
    {
        //transform.position = new Vector2(transform.position.x, playerCords.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {

            AttackableEntity attackableEntity = collision.GetComponent<AttackablePlayer>();
            attackableEntity.RecieveDamage(damage);
        }

        else if (collision.tag == "Pan")
        {
            Destroy(this.gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public float speed = 3.0f;
    public float range = 20f;
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
        while(sign*transform.position.x>sign*(startPos - 1.95 * sign))
        {
            transform.position = Vector2.Lerp(transform.position, 
                new Vector2(startPos - 2 * sign, playerCords.position.y), startSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(moving());

    }
    IEnumerator moving() {

        while (range >= 0f)
        {

            Vector3 translation = new Vector3(direction.x * speed , direction.y * speed, 0) * Time.deltaTime;
            transform.position += translation;
            range -= translation.magnitude;
            yield return new WaitForFixedUpdate(); 
        }
        Destroy(this.gameObject);
        
    }
    private void Update()
    {
        transform.position = new Vector2(transform.position.x, playerCords.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackableEntity attackableEntity = collision.GetComponent<AttackableEntity>();
        if (collision.tag == "Player")
        {
            attackableEntity.RecieveDamage(damage);
        }

        else if (collision.tag == "Pan")
        {
            Destroy(this.gameObject);
        }
    }

}

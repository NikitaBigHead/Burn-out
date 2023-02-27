using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float timeAttack = 0.5f;
    public float delayAttack;

    //public Animator animator;
    public float offset;//смещение связанное с мышью

    public bool isAttacked = false;
    public float damage = 10f;

    public float forceSpeedAttack = 20f;
    public float rangeAttack = 0f;

    public SpriteRenderer spriteRendererPan;

    private Collider2D collider;
    private bool isCanAttack = true;

    private void Awake()
    {
        collider= GetComponent<Collider2D>();
    }
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateZ + offset);

        if (Mathf.Abs(transform.rotation.z) > 0.8f)
        {
            spriteRendererPan.sortingOrder = 3;
        }
        else
        {
            spriteRendererPan.sortingOrder = 4;
        }

        if(Input.GetMouseButtonDown(0) && isCanAttack)
        {
            
            isAttacked = true;
        }
    }

    private void FixedUpdate()
    {
        if(isAttacked)
        {
            spriteRendererPan.color = Color.red;

            isCanAttack= false;
            collider.enabled = true;
            isAttacked= false;
            Invoke("disableCollider", timeAttack);
        }
    }/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            AttackableEntity attackableEntity = collision.gameObject.GetComponent<AttackableEntity>();
            attackableEntity.RecieveDamage(damage);

            if(collision.gameObject.name!= "JumperBag(Clone)")
            {
                Debug.Log("work");
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - 
                    collision.gameObject.transform.position).normalized;

                attackableEntity.RecieveImpulse(rangeAttack,forceSpeedAttack,direction);

                //Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                //rb.AddForce(direction * forceAttack);

                //StartCoroutine( stopGameObject(rb));
            }
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            AttackableEntity attackableEntity = collision.gameObject.GetComponent<AttackableEntity>();
            attackableEntity.RecieveDamage(damage);

            if (collision.gameObject.name != "JumperBag(Clone)")
            {
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                    collision.gameObject.transform.position).normalized;

                attackableEntity.RecieveImpulse(rangeAttack, forceSpeedAttack, direction);
            }
        }
    }
    private void disableCollider()
    {
        spriteRendererPan.color = Color.gray;

        collider.enabled = false;
        Invoke("enableAttack",delayAttack);
    }
    private void enableAttack()
    {
        spriteRendererPan.color = Color.white;
        isCanAttack = true;
    }
    /*
    IEnumerator stopGameObject(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
        yield return new  WaitForSeconds(enemyFlightTime);
    }
    */

}

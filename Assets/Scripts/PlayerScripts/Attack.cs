using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float timeAttack = 0.5f;
    //public Animator animator;
    public float offset;//смещение связанное с мышью
    public bool isAttacked = false;
    public float damage = 10f;
    public SpriteRenderer spriteRendererPan;

    private Collider2D collider;
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

        if(Input.GetMouseButtonDown(0))
        {
            
            isAttacked = true;
        }
    }

    private void FixedUpdate()
    {
        if(isAttacked)
        {
            collider.enabled = true;
            isAttacked= false;
            Invoke("disableCollider", timeAttack);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AttackableEntity>().RecieveDamage(damage);
        }
    }

    private void disableCollider()
    {
        collider.enabled = false;
    }

}

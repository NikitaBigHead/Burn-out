using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f); 
            for(int i = 0;i< colliders.Length;i++) {
                if (colliders[i].tag == "Enemy")
                {
                    colliders[i].GetComponent<AttackableEntity>().RecieveDamage(damage);
                }
                if (colliders[i].tag == "Rock")
                {

                }
            }
            isAttacked= false;
        }
    }

}

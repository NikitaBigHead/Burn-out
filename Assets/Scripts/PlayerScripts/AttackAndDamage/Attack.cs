using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public List<AudioClip> clipList;

    public float timeAttack = 0.5f;
    public float delayAttack;

    public Animator animator;

    public string animation;
    protected int layer = 0;
    public float offset;//смещение связанное с мышью

    public bool isAttacked = false;
    public float damage = 10f;

    public float forceSpeedAttack = 20f;
    public float rangeAttack = 0f;

    public SpriteRenderer spriteRendererPan;

    protected Collider2D collider;
    protected bool isCanAttack = true;

    AudioSource audioSource;

    private void Awake()
    {
        collider= GetComponent<Collider2D>();
        if (animation == "Bag") layer = 1;

        audioSource = transform.parent.GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(clipList[Random.Range(0,clipList.Count)]);

            spriteRendererPan.color = Color.red;
            animator.Play(animation,layer);
            isCanAttack = false;
            collider.enabled = true;
            isAttacked= false;
            Invoke("disableCollider", timeAttack);
        }
    }
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
    protected void disableCollider()
    {
        animator.Play("Empty");
        spriteRendererPan.color = Color.gray;

        collider.enabled = false;
        Invoke("enableAttack",delayAttack);
    }
    protected void enableAttack()
    {
        isCanAttack = true;
    }

}

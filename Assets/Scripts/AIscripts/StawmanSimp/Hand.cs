using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float attackDelay = 0.8f;
    public float damage = 10f;
    public float attackSpeed = 3f;
    public float offset = 1.8f;

    private float currentAngle = 0f;

    private Vector2 direction;

    private Vector3 defaultPos;
    private Quaternion defaultRot;
    private Collider2D hitbox;


    private float endAnglePosHit;
    private float hitAngle = 170f;

    private void Start()
    {
        defaultPos = transform.localPosition;
        defaultRot = transform.rotation;
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    public float PerformAttack(Vector2 direction)
    {
        this.direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x);

        //transform.localPosition = new Vector3(direction.x * offset, direction.y * offset, defaultPos.z);

        //transform.localPosition = new Vector3(direction.x , direction.y, defaultPos.z);

        //transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f);
        endAnglePosHit = angle;

        currentAngle = endAnglePosHit + hitAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle * Mathf.Rad2Deg);
        //transform.Rotate(0f, 0f, currentAngle);

        hitbox.enabled = true;
        StartCoroutine(Attack());
        return attackDelay;
    }
    public IEnumerator Attack()
    {
        while (currentAngle > endAnglePosHit )
        {
            float difAngle = attackSpeed;
            //transform.rotation = Quaternion.Euler(0f, 0f, currentAngle * Mathf.Rad2Deg);
            transform.Rotate(new Vector3(0f, 0f, difAngle));
            currentAngle -= difAngle;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("End");
        hitbox.enabled = false;
        transform.localPosition = defaultPos;
        transform.rotation = defaultRot;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            other.GetComponent<AttackableEntity>().RecieveDamage(damage);
        }
    }
    
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float attackDelay = 0.8f;
    public float damage = 10f;

    public float offset = 1.8f;

    private float currentPos = 0f;

    private Vector2 direction;

    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private Collider2D hitbox;

    private void Start()
    {
        defaultPos = transform.localPosition;
        defaultRot = transform.rotation;
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    public float PerformAttack(Vector2 direction)
    {
        this.direction = direction;
        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.localPosition = new Vector3(direction.x * offset, direction.y * offset, defaultPos.z);
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f);

        currentPos = 0f;
        hitbox.enabled = true;
        //StartCoroutine(Attack());
        return attackDelay;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hitbox.enabled = false;
        transform.localPosition = defaultPos;
        transform.rotation = defaultRot;

        other.GetComponent<AttackableEntity>().RecieveDamage(damage);

    }
    /*
    public IEnumerator Attack()
    {
        while (currentPos < attackRange)
        {
            Vector2 movement = direction * attackSpeed;
            transform.localPosition += new Vector3(movement.x, movement.y, 0);
            currentPos += movement.magnitude;
            yield return new WaitForFixedUpdate();
        }
        hitbox.enabled = false;
        transform.localPosition = defaultPos;
        transform.rotation = defaultRot;
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFork : Weapon
{
    //public float attackDelay = 1.5f;
    public float attackRange = 2;
    public float attackSpeed = 0.1f;

    public float offset = 0f;

    private float currentPos = 0f;

    private Vector2 direction;

    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private Collider2D hitbox;

    private void Start()
    {
        defaultPos = transform.localPosition;
        defaultRot = transform.rotation;
        hitbox = GetComponentInChildren<Collider2D>();
        hitbox.enabled = false;
    }

    public override float PerformAttack(Vector2 direction)
    {
        this.direction = direction;
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.localPosition = new Vector3(direction.x * offset, direction.y * offset, defaultPos.z);
        transform.rotation = Quaternion.Euler(0f, 0f, angle*Mathf.Rad2Deg - 90f);
        currentPos = 0f;
        hitbox.enabled = true;
        StartCoroutine(Attack());
        return attackDelay;
    }

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
}

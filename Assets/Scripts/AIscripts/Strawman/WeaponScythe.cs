using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScythe : Weapon
{
    //public float attackDelay = 1.5f;
    public float attackRange = 2;
    public float attackSpeed = 0.1f;
    public float attackHalfAngle = 45f;

    public float offset = 0f;

    private float rotationAngleSpeed;

    private int flip = 1;

    private float currentAngle;
    private float endAngle;

    private Vector2 direction;

    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private Collider2D hitbox;
    private SpriteRenderer sprite;

    private void Start()
    {
        defaultPos = transform.localPosition;
        defaultRot = transform.rotation;
        hitbox = GetComponentInChildren<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        hitbox.enabled = false;
    }

    public override float PerformAttack(Vector2 direction)
    {
        Vector2 startDirection;
        Vector2 endDirection;
        if (direction.x > 0)
        {
            sprite.flipX = true;
            currentAngle = attackHalfAngle;
            endAngle = -attackHalfAngle;

            startDirection = Quaternion.AngleAxis(currentAngle, Vector3.forward) * direction;
            endDirection = Quaternion.AngleAxis(endAngle, Vector3.forward) * direction;

            this.direction = Quaternion.AngleAxis(-90f, Vector3.forward) * startDirection;
            flip = -1;
        }
        else
        {
            sprite.flipX = false;
            currentAngle = -attackHalfAngle;
            endAngle = attackHalfAngle;

            startDirection = Quaternion.AngleAxis(currentAngle, Vector3.forward) * direction;
            endDirection = Quaternion.AngleAxis(endAngle, Vector3.forward) * direction;

            this.direction = Quaternion.AngleAxis(90f, Vector3.forward) * startDirection;
            flip = 1;
        }
        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.localPosition = new Vector3(startDirection.x * offset, startDirection.y * offset, defaultPos.z);
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f + currentAngle);
        hitbox.enabled = true;

        rotationAngleSpeed = endAngle * 2f / (attackHalfAngle * 2f * Mathf.Deg2Rad * offset / attackSpeed);

        StartCoroutine(Attack());

#if DEBUG
        Vector2 startPos = transform.parent.position;
        Debug.DrawLine(startPos + startDirection * offset * 0.25f, startPos + startDirection * offset * 4f, Color.green, attackDelay);
        Debug.DrawLine(startPos + endDirection * offset * 0.25f, startPos + endDirection * offset * 4f, Color.green, attackDelay);
#endif

        return attackDelay;
    }

    public IEnumerator Attack()
    {
        while (currentAngle * flip < endAngle * flip)
        {
            direction = Quaternion.AngleAxis(rotationAngleSpeed, Vector3.forward) * direction;
            Vector2 movement = direction * attackSpeed;

#if DEBUG
            Vector2 startPos = transform.parent.position;
            Debug.DrawLine(transform.position, transform.position + new Vector3(movement.x, movement.y, defaultPos.z), Color.yellow, attackDelay);
#endif
            
            transform.localPosition += new Vector3(movement.x, movement.y, defaultPos.z);
            transform.Rotate(0, 0, rotationAngleSpeed);
            currentAngle += rotationAngleSpeed;
            yield return new WaitForFixedUpdate();
        }
        hitbox.enabled = false;
        transform.localPosition = defaultPos;
        transform.rotation = defaultRot;
    }
}

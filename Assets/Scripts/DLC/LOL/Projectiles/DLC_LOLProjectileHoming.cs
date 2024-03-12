using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLProjectileHoming : MonoBehaviour
{
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;
        direction = direction / distance;
        transform.position += new Vector3(direction.x, direction.y) * speed * Time.deltaTime;
        speed += acceleration * Time.deltaTime;
        if (Mathf.Abs(speed) > maxSpeed) speed = maxSpeed * Mathf.Sign(speed);
        if (distance <= speed * Time.deltaTime)
        {
            OnReachTarget();
        }
    }

    protected virtual void OnReachTarget()
    {
        Destroy(gameObject);
    }

    public DLC_LOLEntityController source;
    public Transform target;
    public float damage;
    public DLC_LOLDamageType damageType;
    public float speed;
    public float acceleration;
    public float maxSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) return;
        if (collision.CompareTag("Entity"))
        {
            collision.GetComponent<DLC_LOLEntityController>().ReceiveDamage(damage, damageType, source);
        }
    }
}

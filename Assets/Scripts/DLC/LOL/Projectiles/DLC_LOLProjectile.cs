using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLProjectile : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(direction.x, direction.y) * speed * Time.deltaTime;
        currentRange += speed * Time.deltaTime;
        speed += acceleration * Time.deltaTime;
        if (Mathf.Abs(speed) > maxSpeed) speed = maxSpeed * Mathf.Sign(speed);
        if (currentRange >= range)
        {
            OnReachMaxRange();
        }
    }

    protected virtual void OnReachMaxRange()
    {
        Destroy(gameObject);
    }

    public DLC_LOLEntityController source;
    public float damage;
    public DLC_LOLDamageType damageType;
    public Vector2 direction;
    public float speed;
    public float acceleration;
    public float range;
    public float maxSpeed;

    private float currentRange = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) return;
        if (collision.CompareTag("Entity")) 
        {
            collision.GetComponent<DLC_LOLEntityController>().ReceiveDamage(damage, damageType, source);
        }
    }
}

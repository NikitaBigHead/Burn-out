using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float speed = 1.0f;
    public float size = 1.0f;
    public float range = 1.0f;
    public Vector2 direction = Vector2.zero;
    public float damage = 5f;
    public float cashRange; 

    public bool isPLayerRecaptured = false;
    public void Launch(float speed, float size, float range, Vector2 direction)
    {
        this.speed = speed;
        this.size = size;
        transform.localScale = new Vector3(size, size, 1.0f);
        this.cashRange = range;
        this.range = range;
        this.direction = direction;

    }

    protected void FixedUpdate()
    {
        Vector3 translation = new Vector3(direction.x * speed, direction.y * speed, 0);
        transform.position += translation;
        range -= translation.magnitude;
        if (range <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackableEntity attackableEntity = collision.GetComponent<AttackableEntity>();
        if (collision.tag == "Player")
        {
            attackableEntity.RecieveDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Enemy" && isPLayerRecaptured)
        {
            attackableEntity.RecieveDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Pan")
        {
            if (this.gameObject.tag == "Rock")
            {
                direction = new Vector2(-direction.x, -direction.y);
                range = cashRange;
                isPLayerRecaptured = true;
            }
            else
            {
                Destroy(this.gameObject);
            }


        }
        else if(collision.tag == "Bag")
        {
            Destroy(this.gameObject);
        }
        
    }
        
}

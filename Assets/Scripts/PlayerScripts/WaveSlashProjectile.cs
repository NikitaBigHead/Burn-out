using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSlashProjectile : MonoBehaviour
{
    public float damage = 3f;
    public float waveSpeed = 0f;
    public float waveRange = 1f;
    public Vector2 direction = Vector2.zero;

    public float max_size = 2f;

    private float currentRange;

    [SerializeField]
    Collider2D collider2d;

    private void Start()
    {
        if (collider2d == null)
        {
            collider2d = GetComponent<Collider2D>();
        }
    }

    public void Launch(Vector3 position, Quaternion rotation, float speed, float range)
    {
        this.direction = rotation * new Vector2(0, -1);
        this.waveRange = range;
        this.waveSpeed = speed;
        this.transform.position = position;
        this.transform.rotation = rotation * Quaternion.Euler(new Vector3(0, 0, 180));
    }

    private void FixedUpdate()
    {
        if (currentRange < waveRange)
        {
            this.transform.position += new Vector3(direction.x, direction.y, 0) * waveSpeed;
            currentRange += waveSpeed;
            this.transform.localScale = new Vector3(max_size * (currentRange / waveRange), 1f, 1f);
        } else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackableEntity entity = collision.GetComponent<AttackableEntity>();
        if (entity != null)
        {
            entity.RecieveDamage(damage);
        }
    }
}

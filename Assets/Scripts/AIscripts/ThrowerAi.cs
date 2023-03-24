using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerAi : MonoBehaviour
{

    [SerializeField]
    private SimpleProjectile projectile;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 spawnOffset = Vector3.zero;

    public float throwDelay = 1f;

    public float projectileSize = 0.4f;
    public float projectileSpeed = 0.2f;
    public float projectileRange = 10f;
    public float damage = 0;

    private bool canThrow = true;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    void Update()
    {
        if (canThrow) 
        {
            Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
            SimpleProjectile spawnedProjectile = Instantiate(projectile, transform.position + spawnOffset, Quaternion.identity);
            spawnedProjectile.Launch(projectileSpeed, projectileSize, projectileRange, direction);
            if (damage > 0)
            {
                spawnedProjectile.damage = damage;
            }
            canThrow = false;
            Invoke("ResetThrow", throwDelay);
        }
    }

    void ResetThrow()
    {
        canThrow = true;
    }
}

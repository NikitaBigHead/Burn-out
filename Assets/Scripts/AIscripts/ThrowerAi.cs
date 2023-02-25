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
            Instantiate(projectile, transform.position + spawnOffset, Quaternion.identity).Launch(projectileSpeed, projectileSize, projectileRange, direction);

            canThrow = false;
            Invoke("ResetThrow", throwDelay);
        }
    }

    void ResetThrow()
    {
        canThrow = true;
    }
}

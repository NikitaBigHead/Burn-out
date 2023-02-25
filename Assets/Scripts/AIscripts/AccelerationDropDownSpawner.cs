using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationDropDownSpawner : MonoBehaviour
{
    [SerializeField]
    private AccelerationDropDownProjectile projectile;

    [SerializeField]
    private Transform target;

    public float dropDownDelay = 2f;
    
    public float projectileSize = 1.0f;
    public float projectileSpeed = 0.1f;

    public float spawnHeight = 0f;

    public float peakHeight = 10f;
    public float peakOffset = 0.5f;

    private bool canSpawn = true;

    private void Update()
    {
        if (canSpawn)
        {
            Vector2 newTarget = new Vector2(target.position.x, target.position.y);

            canSpawn = false;
            Vector2 selfPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 peakPos = selfPos + (newTarget - selfPos) * peakOffset;
            peakPos = new Vector2(peakPos.x, peakPos.y + peakHeight);
            Instantiate(projectile, new Vector3(0, 0, 0), Quaternion.identity).Launch
                (
                    selfPos, peakPos, newTarget, spawnHeight, projectileSpeed, projectileSize
                );
            Invoke("ResetSpawn", dropDownDelay);
        }
    }

    public void ResetSpawn()
    {
        canSpawn = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1);
        Gizmos.DrawIcon(transform.position, "Assets/Sprites/OtherSprites/SnowBallSpawnerIcon.png", true);
    }
#endif
}

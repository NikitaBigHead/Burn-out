using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownSpawner : MonoBehaviour
{
    [SerializeField] 
    private DropDownProjectile projectile;

    [SerializeField]
    private Transform target;

    public float dropDownDelay = 2f;

    public float projectileSize = 1.0f;
    public float projectileSpeed = 0.1f;
    public float projectileHeight = 20f;

    private bool canSpawn = true;

    private void Update()
    {
        if (canSpawn)
        {
            Vector2 newTarget = new Vector2(target.position.x, target.position.y);

            canSpawn = false;
            Instantiate(projectile, new Vector3(0, 0, 0), Quaternion.identity).Launch(0.1f, 1f, projectileHeight, newTarget);
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
        Gizmos.DrawIcon(transform.position, "Assets/Sprites/OtherSprites/SpawnerIcon.png", true);
    }
#endif
}

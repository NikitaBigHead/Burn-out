using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawmanRangerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    public float spawnDelay = 5f;
    // 0 - not limited
    public int maxAlive = 3;
    // 0 - not limited
    public int maxSpawned = 0;

    private int currentSpawned = 0;

    private bool canSpawn = false;

    private void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        currentSpawned = 0;
        canSpawn = true;
        StartCoroutine(Spawn());
    }

    public void StopSpawn()
    {
        canSpawn = false;
    }

    IEnumerator Spawn()
    {
        while (canSpawn)
        {
            if (maxAlive == 0 || transform.childCount < maxAlive)
            {
                GameObject new_mob = Instantiate(enemy, transform);
                new_mob.transform.localPosition = new Vector3(0, 0, 0);
                new_mob.GetComponent<RangerAi>().throwDelay = 3f;
                new_mob.GetComponent<RangerAi>().projectileRange = 7f;
                new_mob.GetComponent<RangerAi>().projectileSpeed = 0.12f;
                new_mob.GetComponent<AttackableEntity>().health = 20;
                currentSpawned++;
                if (maxSpawned != 0 && currentSpawned >= maxSpawned) StopSpawn();
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Assets/Sprites/OtherSprites/SpawnerIcon.png", true);
    }
#endif
}

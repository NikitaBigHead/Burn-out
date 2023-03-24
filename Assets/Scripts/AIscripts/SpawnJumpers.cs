using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJumpers : MonoBehaviour
{
    [SerializeField]
    private GameObject jumper;

    [SerializeField]
    private Vector3 spawnOffset = Vector3.zero;

    public float spawnDelay = 3f;
    public int spawnMaxCount = 6;

    private int spawnCount = 0;
    private bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        StartCoroutine(SpawnJumper());
    }
    public void substractSpawner()
    {
        spawnCount--;
    }
    IEnumerator SpawnJumper()
    {
        while (canSpawn)
        {
            if (spawnCount != spawnMaxCount)
            {
                Instantiate(jumper, transform.position + spawnOffset, Quaternion.identity);
                spawnCount++;
            }
           
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

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

    private bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        StartCoroutine(SpawnJumper());
    }

    IEnumerator SpawnJumper()
    {
        while (canSpawn)
        {
            Instantiate(jumper, transform.position + spawnOffset, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject finalPlatformPrefab;

    public TextMeshProUGUI text;
    public int countPlatforms = 10;
    public int scorePerPlatform = 10;
    private int score = -1;

    private float startSpawnPostionPlatforms = -1f;
    private void Awake()
    {
#if DEBUG
        GameObject.FindWithTag("Debug")?.gameObject.SetActive(true);
#else
    GameObject.FindWithTag("Debug")?.gameObject.SetActive(false);
#endif


        for(int i = 0; i < countPlatforms; i++)
        {
            SpawnPlatform(platformPrefab);
        }
        SpawnPlatform(finalPlatformPrefab);
    }


    void SpawnPlatform(GameObject platformPrefab)
    {
        float x = Random.Range(-4f, 4f);
        float y = startSpawnPostionPlatforms;
        Instantiate(platformPrefab, new Vector3(x, y, 1), quaternion.identity);
        startSpawnPostionPlatforms += Random.Range(3.5f, 5f);
    }


    public void SkipGame()
    {
        Debug.Log("skip");
        GameObject player = GameObject.FindGameObjectWithTag("Body");
        player.transform.position = new Vector3(0, startSpawnPostionPlatforms, 0);
    }
 

    public void AddScore()
    {
        // Add score when the player lands on a platform
        score += scorePerPlatform;
        text.text = String.Format("{0}/{1}", score, countPlatforms);
    }
}

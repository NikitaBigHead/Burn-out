using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class SnowballSpawner : MonoBehaviour
{
    public GameObject snowBallPrefab;
    public Transform playerCords;
    private float interval = 5f;

    public float height = 10f;

    public float projectileSize = 1f;
    public float projectileSpeed = 0.2f;
    public float projectileRange = 40f;

    public GameObject attentionPrefab;
    void Start()
    {
        StartCoroutine("spawn");
    }


    IEnumerator  dropSnowBall(float interval, Transform cords)
    {
        yield return new WaitForSeconds(interval-0.2f);
        GameObject snowBall = Instantiate(snowBallPrefab,
         cords.position
         , Quaternion.identity);
        Vector2 direction = new Vector2(0, -1f);
        snowBall.GetComponent<SimpleProjectile>().Launch(projectileSpeed, projectileSize, projectileRange, direction);


    }
    IEnumerator  spawn()
    {
        while (true)
        {

            Vector2 cords = new Vector2(playerCords.position.x, Camera.main.transform.position.y + 5.2f);
            GameObject attentionObject = Instantiate(attentionPrefab,
               cords,
                Quaternion.identity);

            attentionObject.transform.parent = Camera.main.transform;
            Attention attention= attentionObject.GetComponent<Attention>();
            attention.Interval =2f;
            attention.StartAttention();

            StartCoroutine( dropSnowBall(2f, attention.transform));

            yield return new WaitForSeconds(interval);
        }

    }
}

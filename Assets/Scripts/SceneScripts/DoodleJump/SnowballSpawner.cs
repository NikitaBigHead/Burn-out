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
    public float interval = 3f;

    public float height = 10f;

    public float projectileSize = 1f;
    public float projectileSpeed = 0.2f;
    public float projectileRange = 40f;

    public GameObject attentionPrefab;
    void Start()
    {
        StartCoroutine("spawn");
    }



    IEnumerator  spawn()
    {
        while (true)
        {
            GameObject snowBall = Instantiate(snowBallPrefab,
                new Vector2(playerCords.position.x, playerCords.position.y + height)
                , Quaternion.identity);
            Vector2 direction = new Vector2(0, -1f);
            snowBall.GetComponent<SimpleProjectile>().Launch(projectileSpeed, projectileSize, projectileRange, direction);

            GameObject attentionObject = Instantiate(attentionPrefab,
                new Vector2(playerCords.position.x, Camera.main.transform.position.y + 5.2f),
                Quaternion.identity);

            attentionObject.transform.parent = Camera.main.transform;
            Attention attention= attentionObject.GetComponent<Attention>();
            attention.Interval = interval - 2.5f;
            attention.StartAttention();

            yield return new WaitForSeconds(interval);
        }

    }
}

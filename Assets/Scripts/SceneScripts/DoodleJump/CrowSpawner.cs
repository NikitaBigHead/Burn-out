using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CrowSpawner : MonoBehaviour
{
    public GameObject crowPrefab;
    public Transform playerCords;
    public float interval = 10f;

    private float posX = 9.53f;
    private void Start()
    {
        StartCoroutine(spawn());
    }
    IEnumerator spawn()
    {
        while (true)
        {
            float sign = setRandoNumberSign();
            GameObject crow = Instantiate(crowPrefab,
                new Vector2(posX* sign, playerCords.position.y)
                , Quaternion.identity);
            crow.GetComponent<Crow>().Launch(sign);

            yield return new WaitForSeconds(interval);
        }

    }

    private float setRandoNumberSign()
    {
        float num = Random.Range(-10, 10);
        if (num <= 0)
        {
            return -1;
        }
        return 1;
    }
}

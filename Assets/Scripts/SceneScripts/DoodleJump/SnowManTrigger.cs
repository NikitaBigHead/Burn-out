using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManTrigger : MonoBehaviour
{
    public GameObject snowman;

    private GameObject spawn1;
    private GameObject spawn2;
    private void Awake()
    {
        spawn1 = Camera.main.transform.GetChild(2).gameObject;
        spawn2 = Camera.main.transform.GetChild(3).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            snowman.GetComponent<ThrowerAi>().enabled = true;
            spawn1.SetActive(false);
            spawn2.SetActive(false);
            Destroy(this.gameObject);
        }

    }
}

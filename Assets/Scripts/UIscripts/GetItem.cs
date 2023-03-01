using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private GameObject player;

    private GameObject Pan;
    private GameObject Pancake;
    private GameObject Bag;
    private GameObject Key;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Pan = player.transform.Find("Pan").gameObject;
        Bag = player.transform.Find("Bag").gameObject;

    }
    
    private void clearItems()
    {
        //Destroy(player.GetComponentInChildren<Transform>());
        Pan.active = false;
        Bag.active = false;
    }

    public void getPancake()
    {
        clearItems();

    }
    public void getPan()
    {
        clearItems();
        Pan.active = true;
    }
    public void getBag()
    {
        clearItems();
        Bag.active = true;
    }
    public void getKey()
    {
        clearItems();
    }

}

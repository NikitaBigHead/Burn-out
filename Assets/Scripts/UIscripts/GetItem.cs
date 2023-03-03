using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private GameObject player;
    //private GameObject k

    private GameObject Pan;
    private GameObject Pancake;
    private GameObject Bag;
    private GameObject Key;

    private UIGameObjectHandler handlerCounter;
    private TextMeshProUGUI pancakeCount;
    private TextMeshProUGUI keyCount;

    private Pancake pancake;
    private Key key;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Pan = player.transform.Find("Pan").gameObject;
        Bag = player.transform.Find("Bag").gameObject;
        Pancake = player.transform.Find("Pancake").gameObject;
        Key = player.transform.Find("Key").gameObject;

        handlerCounter = GetComponentInParent<UIGameObjectHandler>();
        pancakeCount=handlerCounter.gameObjects[4].GetComponent<TextMeshProUGUI>();
        keyCount = handlerCounter.gameObjects[5].GetComponent<TextMeshProUGUI>();

        pancake = Pancake.GetComponent<Pancake>();
        key = Key.GetComponent<Key>();
    }
    
    private void clearItems()
    {
        //Destroy(player.GetComponentInChildren<Transform>());
        Pan.active = false;
        Bag.active = false;
        Pancake.active = false;
        Key.active = false;
    }

    public void getPancake()
    {
        
        clearItems();
        Pancake.active = true;
        pancake.count = Convert.ToInt32( pancakeCount);
        
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
        Key.active = true;
        key.count = Convert.ToInt32(keyCount);

    }

}

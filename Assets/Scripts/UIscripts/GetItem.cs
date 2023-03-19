using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
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
    private SetitemsUI setitems;
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

        setitems = GetComponentInParent<SetitemsUI>();
        handlerCounter = GetComponentInParent<UIGameObjectHandler>();

        pancakeCount=handlerCounter.gameObjects[4].GetComponent<TextMeshProUGUI>();
        keyCount = handlerCounter.gameObjects[5].GetComponent<TextMeshProUGUI>();

        pancake = Pancake.GetComponent<Pancake>();
        key = Key.GetComponent<Key>();
    }
    
    public void clearItems()
    {
        //Destroy(player.GetComponentInChildren<Transform>());
        Pan.active = false;
        Bag.active = false;
        Pancake.active = false;
        Key.active = false;
        PlayerData.selectedItem = "none";
    }

    public void getPancake()
    {
        clearItems();
        Pancake.active = true;
        //pancake.Text = pancakeCount;
        pancake.SetItem = setitems;
        PlayerData.selectedItem = "pancake"; 
    }
    public void getPan()
    {
        clearItems();
        Pan.active = true;
        PlayerData.selectedItem = "pan";
    }
    public void getBag()
    {
        clearItems();
        Bag.active = true;
        PlayerData.selectedItem = "bag";
    }
    public void getKey()
    {
        clearItems();
        Key.active = true;
        //key.Text = keyCount;
        key.SetItem = setitems;
        PlayerData.selectedItem = "key";

    }

}

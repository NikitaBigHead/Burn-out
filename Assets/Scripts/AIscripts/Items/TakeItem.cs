using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.Actions.MenuPriority;

public class TakeItem : MonoBehaviour
{
    public string text = "[E] подобрать";
    public string keyItem;
    protected TextMeshProUGUI hint;
    protected GameObject player;
    protected SetitemsUI setItem;

    public int itemId;

    protected bool isTrigger = false;
    protected void Awake()
    {
        PlayerData.RegisterPickUpItem(this);
        player = GameObject.FindGameObjectWithTag("Player");
        hint = player.GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent<TextMeshProUGUI>();
        try
        {
            setItem = GameObject.FindGameObjectWithTag("UI").GetComponent<SetitemsUI>();
        }
        catch(NullReferenceException ex)
        {
            setItem= null;
        }

    }

    protected void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            hint.text = text;
            //StartCoroutine(waitPlayerDecide());
            isTrigger= true;
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = "";
            //StopCoroutine(waitPlayerDecide());
            isTrigger= false;
        }
    }

    protected void  Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            PlayerData.PickUpPickUpItem(this);
            Item item = new Item(keyItem);
            //PlayerData.listKey = item;
            PlayerData.AddItem(keyItem);
            if(setItem!=null)setItem.addItem(ref item);
            Destroy(this.gameObject);

        }
    }/*
    IEnumerator waitPlayerDecide()
    {
            if (Input.GetKeyDown(KeyCode.E))
        {
            Item item = new Item(keyItem, id);
            PlayerData.listKey = item;
            setItem.addItem(ref item);
            Destroy(this.gameObject);

        }
            yield return new  WaitForEndOfFrame() ;
        
    }
    */
}

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
    private TextMeshProUGUI hint;
    private GameObject player;
    private SetitemsUI setItem;

    private bool isTrigger = false;
    private void Awake()
    {
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

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            hint.text = text;
            //StartCoroutine(waitPlayerDecide());
            isTrigger= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = "";
            //StopCoroutine(waitPlayerDecide());
            isTrigger= false;
        }
    }

    private void  Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            Item item = new Item(keyItem);
            PlayerData.listKey = item;
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

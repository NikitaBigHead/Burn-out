using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public string text = "Нажмите [E], чтобы открыть замок";
    private TextMeshProUGUI hint;
    private GameObject player;
    private int needKeys = 3;
    private bool triggered;
    private GameObject door;

    private SetitemsUI setitems;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setitems =  GameObject.FindGameObjectWithTag("UI").GetComponent<SetitemsUI>();
        hint = player.GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent<TextMeshProUGUI>();
        door = transform.parent.transform.GetChild(2).gameObject;//door
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hint.text = "";
            triggered = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hint.text = text;
            triggered = true;
        }

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && triggered)
        {
            setitems.subtractItem("key");
            needKeys--;
            text = string.Format("Осталось {0} ключей,чтобы открыть замок", needKeys);
            if (needKeys == 0)
            {
                door.active = true;
                gameObject.active = false;
            }
        }   
    }
}

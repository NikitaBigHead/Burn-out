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
        needKeys -= PlayerData.keysInDoor;
        player = GameObject.FindGameObjectWithTag("Player");
        setitems =  GameObject.FindGameObjectWithTag("UI").GetComponent<SetitemsUI>();
        hint = player.GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent<TextMeshProUGUI>();
        door = transform.parent.transform.GetChild(2).gameObject;//door
    }
    private void Start()
    {
        if(PlayerData.isOpenLock)
        {
            door.active = true;
            gameObject.active = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hint.text = "";
            triggered = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            if (setitems.subtractItem("key"))
            {
                needKeys--;
                PlayerData.keysInDoor++;
                text = string.Format("Осталось {0} ключей,чтобы открыть замок", needKeys);
                hint.text = text;
                if (needKeys == 0)
                {
                    PlayerData.isOpenLock = true;
                    door.active = true;
                    gameObject.active = false;
                }
            } else hint.text = "Нечем открыть замок. Нужен ключ!";
        }   
    }
}

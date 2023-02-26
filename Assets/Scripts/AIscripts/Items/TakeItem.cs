using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class TakeItem : MonoBehaviour
{
    public string text = "[E] подобрать";
    public string keyItem;
    public string id;
    private TextMeshProUGUI hint;
    private GameObject player;
    private SetitemsUI setItem;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setItem = GameObject.FindGameObjectWithTag("UI").GetComponent<SetitemsUI>();

        hint = player.GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent<TextMeshProUGUI>();
        //panel = GameObject.FindGameObjectWithTag("Hint");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            hint.text = text;
            StartCoroutine(waitPlayerDecide());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = "";
            StopCoroutine(waitPlayerDecide());
        }
    }


    IEnumerator waitPlayerDecide()
    {
        while(true)
            {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Item item = new Item(keyItem, id);
                PlayerData.listKey = item;
                setItem.addItem(item);

                StopCoroutine(waitPlayerDecide());
                Destroy(this.gameObject);
                break;
            }
            yield return null;
        }

    }
}

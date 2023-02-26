using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TakeItem : MonoBehaviour
{
    public string text = "[E] подобрать";
    public string keyItem;
    private TextMeshProUGUI hint;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Debug.Log(player.GetComponent<PlayerGameObjectHolder>().gameObjects[0]);
        Debug.Log(player.GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent< TextMeshProUGUI > ());
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
                Debug.Log("take it");
                Destroy(this.gameObject);
                break;
            }
            yield return null;
        }

    }
}

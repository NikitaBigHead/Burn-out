using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string Scene;

    public bool open = false;

    public string text = "Íאזלטעו [E], קעמבû חאיעט ג המל";
    private TextMeshProUGUI hint;
    private bool triggered;

    private void Awake()
    {
        hint = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && open)
            SceneLoader.LoadScene(Scene);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = "";
            triggered = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = text;
            triggered = true;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggered)
        {
            open = true;
        }
    }
}

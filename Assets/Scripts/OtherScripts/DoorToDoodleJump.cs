using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorToDoodleJump : MonoBehaviour
{
    public string text = "Нажмите [E], чтобы залезть";
    private TextMeshProUGUI hint;
    private GameObject player;
    private bool triggered;

    private void Awake()
    {
        if (PlayerData.pillarKeyRecieved) Destroy(this);
        player = GameObject.FindGameObjectWithTag("Player");
        hint = player.GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = "";
            triggered = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            PlayerData.healthWhenEnterDoodle = PlayerData.playerCurrentHealth;
            SceneLoader.LoadScene("Doodle Jump");
            gameObject.active = false;
        }
    }
}

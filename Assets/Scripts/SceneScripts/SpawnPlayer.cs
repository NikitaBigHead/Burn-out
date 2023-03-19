using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject ui;

    private AttackablePlayer attackable;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ui = GameObject.FindGameObjectWithTag("UI");
        attackable = player.GetComponent<AttackablePlayer>();
        attackable.health = PlayerData.playerCurrentHealth;
        attackable.maxHealth = PlayerData.playerMaxHealth;
        player.transform.position = PlayerData.nextScenePosition;
        switch (PlayerData.selectedItem)
        {
            case "pan":
                ui.GetComponentInChildren<GetItem>().getPan();
                break;
            case "bag":
                ui.GetComponentInChildren<GetItem>().getBag();
                break;
            case "pancake":
                ui.GetComponentInChildren<GetItem>().getPancake();
                break;
            case "key":
                ui.GetComponentInChildren<GetItem>().getKey();
                break;
            default:
                ui.GetComponentInChildren<GetItem>().clearItems();
                break;
        }
        
    }
}

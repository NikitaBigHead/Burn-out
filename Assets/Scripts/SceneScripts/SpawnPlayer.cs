using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;

    private AttackablePlayer attackable;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackable = player.GetComponent<AttackablePlayer>();
        attackable.health = PlayerData.playerCurrentHealth;
        attackable.maxHealth = PlayerData.playerMaxHealth;
        player.transform.position = PlayerData.nextScenePosition;
    }
}

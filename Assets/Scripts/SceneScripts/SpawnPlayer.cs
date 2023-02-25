using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;

    private AttackableEntity attackable;
    private void Awake()
    {
        attackable = player.GetComponent<AttackableEntity>();
        attackable.health = PlayerData.prefab.GetComponent<AttackableEntity>().health;
    }
}

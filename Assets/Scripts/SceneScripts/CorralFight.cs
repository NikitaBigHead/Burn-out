using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorralFight : MonoBehaviour
{
    /// <summary>
    /// Режим закрытия и открытия дверей и проходов в другие сцены
    /// True - все двери на сцене
    /// False - только выбранные
    /// </summary>
    public bool allDoors = true;
    public List<Door> doors = new List<Door>();

    public GameObject enemies;

    [SerializeField]
    private GameObject keyPosition;

    private int enemyCount = 0;

    private void Awake()
    {
        enemies.SetActive(false);
        keyPosition.SetActive(false);
        keyPosition.GetComponentInChildren<TakeItemEdited>().OnPickUp = (GameObject sender) => { PlayerData.corralKeyRecieved = true; };
        if (PlayerData.corralBossFightComplited) 
        {
            if (!PlayerData.corralKeyRecieved)
            {
                keyPosition.SetActive(true);
            }
            else Destroy(this.gameObject);
        } else
        {
            StartFight();
        }
    }

    private void StartFight()
    {
        if (allDoors)
            foreach (Door door in FindObjectsByType<Door>(FindObjectsSortMode.None))
            {
                door.open = false;
            }
        else
            foreach (Door door in doors)
            {
                door.open = false;
            }
        enemies.SetActive(true);
        foreach (AttackableEntity enemy in enemies.GetComponentsInChildren<AttackableEntity>())
        {
            enemy.actionsOnDeaths.Add((GameObject sender) => { enemyCount--; if (enemyCount == 0) Complite(); });
            enemyCount++;
        }
    }

    public void Complite()
    {
        PlayerData.corralBossFightComplited = true;
        enemies.SetActive(false);
        if (!PlayerData.corralKeyRecieved)
        {
            keyPosition.SetActive(true);
        }
        if (allDoors)
            foreach (Door door in FindObjectsByType<Door>(FindObjectsSortMode.None))
            {
                door.open = true;
            }
        else
            foreach (Door door in doors)
            {
                door.open = true;
            }
    }
}

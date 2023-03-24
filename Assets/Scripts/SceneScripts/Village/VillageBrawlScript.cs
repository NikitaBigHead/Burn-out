using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageBrawlScript : MonoBehaviour
{
    /// <summary>
    /// Режим закрытия и открытия дверей и проходов в другие сцены
    /// True - все двери на сцене
    /// False - только выбранные
    /// </summary>
    public bool allDoors = true;
    public List<Door> doors = new List<Door>();

    public GameObject enemies;
    public GameObject enemies2;

    private int enemyCount = 0;

    private void Awake()
    {
        enemies.SetActive(false);
        enemies2.SetActive(false);
        if (PlayerData.villageBrawlComplited)
            if (!PlayerData.pillarKeyRecieved || !PlayerData.castleKeyRecieved || !PlayerData.castleKeyRecieved || PlayerData.villageBrawlPart2Complited) Destroy(gameObject);
    }

    private void Start()
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
        if (PlayerData.villageBrawlComplited) enemies = enemies2;
        enemies.SetActive(true);
        foreach (AttackableEntity enemy in enemies.GetComponentsInChildren<AttackableEntity>())
        {
            enemy.actionsOnDeaths.Add((GameObject sender) => { enemyCount--; if (enemyCount == 0) Complite(); });
            enemyCount++;
        }
    }

    public void Complite()
    {
        if (PlayerData.villageBrawlComplited)
        {
            PlayerData.villageBrawlPart2Complited = true;
        } else
            PlayerData.villageBrawlComplited = true;
        enemies.SetActive(false);
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

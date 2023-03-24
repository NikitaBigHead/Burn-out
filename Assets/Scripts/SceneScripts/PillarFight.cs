using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarFight : MonoBehaviour
{
    /// <summary>
    /// Режим закрытия и открытия дверей и проходов в другие сцены
    /// True - все двери на сцене
    /// False - только выбранные
    /// </summary>
    public bool allDoors = true;
    public List<Door> doors = new List<Door>();

    public GameObject enemies;

    private int enemyCount = 0;

    private void Awake()
    {
        enemies.SetActive(false);
        if (PlayerData.pillarFight) Destroy(this);
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
        enemies.SetActive(true);
        foreach (AttackableEntity enemy in enemies.GetComponentsInChildren<AttackableEntity>())
        {
            enemy.actionsOnDeaths.Add((GameObject sender) => { enemyCount--; if (enemyCount == 0) Complite(); });
            enemyCount++;
        }
    }

    public void Complite()
    {
        PlayerData.pillarFight = true;
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

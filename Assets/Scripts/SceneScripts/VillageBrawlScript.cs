using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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

    private void Awake()
    {
        if (PlayerData.villageBrawlComplited) Destroy(gameObject);
    }

    private void Start()
    {
        if (allDoors)
            foreach (Door door in FindObjectsByType<Door>(FindObjectsSortMode.None))
            {
                door.enabled = false;
            }
        else
            foreach (Door door in doors)
            {
                door.enabled = false;
            }
    }

    public void Complite()
    {
        if (allDoors)
            foreach (Door door in FindObjectsByType<Door>(FindObjectsSortMode.None))
            {
                door.enabled = true;
            }
        else
            foreach (Door door in doors)
            {
                door.enabled = true;
            }
    }
}

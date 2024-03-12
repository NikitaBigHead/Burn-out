using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DLC_StaticData
{
    private static bool initialized = false;

    private static DLC_GameObjectList allEnemies;
    private static GameObject playerPrefab;

    public static void Init(string scene)
    {
        if (!initialized)
        {
            allEnemies = Resources.Load("AllEnemies") as DLC_GameObjectList;
            playerPrefab = Resources.Load("player") as GameObject;
            initialized = true;
        }

        foreach (DLC_EntityData entity in scenesData[scene].entities) 
        {
            GameObject.Instantiate(allEnemies[entity.id], entity.position, Quaternion.identity).GetComponent<DLC_Enemy>().LoadData(entity.data);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            player = GameObject.Instantiate(playerPrefab, playerData.position, Quaternion.identity);
        }
        player.GetComponent<DLC_Entity>().LoadData(playerData);
    }

    public static void Save(string scene)
    {
        if (scenesData.ContainsKey(scene))
        {
            scenesData.Remove(scene);
        }
        List<DLC_EntityData> entities = new List<DLC_EntityData> { };
        foreach (DLC_Enemy entity in GameObject.FindObjectsOfType<DLC_Enemy>())
        {
            entities.Add
            (
                new DLC_EntityData 
                {
                    position = entity.gameObject.transform.position,
                    id = entity.id,
                    data = entity.GetData()
                }
            );
        }
        scenesData.Add(scene, new DLC_SceneData { entities = entities });
    }

    private static DLC_PlayerData playerData = new DLC_PlayerData
    {
        scene = "DLC_ch1",
        position = new Vector3(0, 0, 0),

        stats = new List<Stat> { },
        statsValues = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 } },
        level = 0,
        xp = 0,
        xpToNextLvl = 1,
        skillPoints = 0,
    };

    private static Dictionary<string, DLC_SceneData> scenesData = new Dictionary<string, DLC_SceneData>
    {
        {
            "DLC_ch1",
            new DLC_SceneData
            {
                entities = new List<DLC_EntityData>
                {
                    new DLC_EntityData
                    {
                        position = new Vector3(6, 0, 0),
                        id = 0,
                        data = new DLC_BaseBurnoutEnemyData{ health = 100 }
                    },
                    new DLC_EntityData
                    {
                        position = new Vector3(-6, 0, 0),
                        id = 1,
                        data = new DLC_BaseBurnoutEnemyData{ health = 100 }
                    },
                    new DLC_EntityData
                    {
                        position = new Vector3(9, 6, 0),
                        id = 2,
                        data = new DLC_BaseBurnoutEnemyData{ health = 70 }
                    }
                }
            } 
        } 
    };
}

[Serializable]
public struct DLC_PlayerData
{
    public string scene;
    public Vector3 position;

    // PlayerStats.cs
    public List<Stat> stats;
    public Dictionary<int, int> statsValues;
    public int level;
    public int xp;
    public int xpToNextLvl;
    public int skillPoints;
}

[Serializable]
public struct DLC_SceneData
{
    public List<DLC_EntityData> entities;
}

[Serializable]
public struct DLC_EntityData
{
    public Vector3 position;
    public int id;
    public object data;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData 
{
    //
    // Fight on Village after tutorial
    public static bool villageBrawlComplited = false;
    //
    // Fight on Village after getting 3 keys
    public static bool villageBrawlPart2Complited = false;
    //
    // JumperBag Fight in CorralLocation
    public static bool corralBossFightComplited = false;
    //
    // Corral Key
    public static bool corralKeyRecieved = false;
    //
    // Castle key
    public static bool castleKeyRecieved = false;
    //
    // Pillar key
    public static bool pillarKeyRecieved = false;
    //
    // Pillar fight
    public static bool pillarFight = false;
    //
    // Castle fight
    public static bool castleFight = false;
    //
    // Cutscene State
    public static bool cutsceneComplited = false;
    //
    // Tutorial State
    public static bool tutorialComplited = false;

    //
    // Boss door keys
    public static int keysInDoor = 0;


    //
    // Player Health
    public static float playerCurrentHealth = 100;
    //
    // Player Health
    public static float playerMaxHealth = 100;
    //
    // Selected item
    public static string selectedItem = "none";
    //
    // Next Scene Position
    public static Vector3 nextScenePosition = Vector3.zero;
    //

    public static bool isOpenLock = false;
    static Dictionary<int, bool> pickupItemsOnScene = new Dictionary<int, bool>();

    public static void RegisterPickUpItem(TakeItem item)
    {
        if (item.itemId != 0)
        {
            if (pickupItemsOnScene.ContainsKey(item.itemId))
            {
                item.gameObject.SetActive(pickupItemsOnScene[item.itemId]);
            }
            else
            {
                pickupItemsOnScene.Add(item.itemId, true);
            }
        }
    }

    public static void PickUpPickUpItem(TakeItem item)
    {
        pickupItemsOnScene[item.itemId] = false;
    }

    public static CheckpointManager.Checkpoint checkpoint = CheckpointManager.Checkpoint.StartLocation;

    private static GameObject prefabName;
    //private static List<string> list;
    private static List<Item> list = new List<Item>();

    public static GameObject prefab
    {
        get
        {
            return prefabName;
        }
        set
        {
            prefabName = value;
        }
    }
    
    public static void LoadSavedItems(List<Item> items)
    {
        list = new List<Item>();
        items.ForEach((item) =>
        {
            list.Add((Item)item.Clone());
        });
    }
    public static Item listKey
    {
        set
        {
            if (isItemInList(value)){
                setCount(value);
            }
            list.Add(value);
        }
    }
    public static List<Item> getListKey
    {
        get
        {
            return list;
        }
    }
    private static bool isItemInList(Item item)
    {

        for(int i =0;i< list.Count; i++)
        {
            if(item.key == list[i].key)
            {
                return true;
            }
        }
        return false;
    }
    public static bool isItemInList(string key)
    {

        for (int i = 0; i < list.Count; i++)
        {
            if (key == list[i].key)
            {
                return true;
            }
        }
        return false;
    }
    private static void setCount(Item item)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (item.key == list[i].key)
            {
                item.count++;
            }
        }
    }
    public static int getCountKey(string key)
    {
        int count = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].key == key) count++;
        }
        return count;
    }

    public static Item getItem(string key)
    {
        for(int i = 0;i< list.Count;i++)
        {
            if (list[i].key == key)
            {
                return list[i];
            }
        }
        return null;
    }

    public static bool RemoveItem(string key)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].key == key)
            {
                if (list[i].count == 1)
                {
                    list.RemoveAt(i);
                }
                else
                {
                    list[i].count--;
                }
                return true;
            }
        }
        return false;
    }

    public static void AddItem(string key)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].key == key)
            {
                list[i].count++;
                return;
            }
        }
        list.Add(new Item(key, 1));
    }

    public static int GetCount(string key)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].key == key)
            {
                return list[i].count;
            }
        }
        return 0;
    }


}
public class Item: ICloneable
{
    public int count = 0;
    public string key;
    public Item(string key)
    {
        this.key = key;
    }

    public Item(string key, int count)
    {
        this.key = key;
        this.count = count;
    }

    public object Clone()
    {
        Item clone = new Item(key);
        clone.count = this.count;
        return clone;
    }

    public override string ToString()
    {
        return $"name: {key}, count: {count}";
    }
}

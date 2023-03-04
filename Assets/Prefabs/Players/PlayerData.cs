using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData 
{
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
    

}
public class Item
{
    public int count = 0;
    public string key;
    public Item(string key)
    {
        this.key = key;
    }
}

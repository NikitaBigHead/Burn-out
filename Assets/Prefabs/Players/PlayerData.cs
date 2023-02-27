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
    

}
public class Item
{
    public string id ;
    public string key;
    public Item(string key,string id)
    {
        this.key = key;
        this.id = id;
    }
}

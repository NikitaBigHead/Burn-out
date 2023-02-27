using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetitemsUI : MonoBehaviour
{
    private List<Item> keyItems;
    private List<GameObject> iconsitems;

    private Dictionary<string, GameObject> icons = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> countIcons = new Dictionary<string, GameObject>();

    private string[] keys = {"pan","bag","pancake","key", "pancake", "key" };

    private int countPancake;
    private int countKeys;

    private void Awake()
    {
        keyItems = PlayerData.getListKey;
        iconsitems = GetComponent<UIGameObjectHandler>().gameObjects;
        Debug.Log(iconsitems.Count);

        for (int i = 0; i < (iconsitems.Count - 2); i++)
        {
            icons.Add(keys[i], iconsitems[i]);
        }
        for(int i = (iconsitems.Count - 2); i < iconsitems.Count; i++)
        {
            Debug.Log(i);
            countIcons.Add(keys[i], iconsitems[i]);
        }
    }
    private void Start()
    {
        for (int i = 0; i < keyItems.Count; i++)
        {
            if (icons[keyItems[i].key])
            {
                icons[keyItems[i].key].active = true;
            }
        }
        countPancake =  getCountKey(keyItems,"pancake");
        countKeys = getCountKey(keyItems,"key");
    }
    public void addItem(ref  Item item)
    {

        string key = item.key;
        if(key == "pancake")
        {
            if (countPancake == 0)
            {
                icons[key].active = true;
            }
            countPancake++;
            countIcons[key].GetComponent<TextMeshProUGUI>().text = countPancake.ToString();

        }
        else if (key == "key")
        {
            if (countKeys == 0)
            {
                icons[key].active = true;
            }
            countPancake++;
            countIcons[key].GetComponent<TextMeshProUGUI>().text = countPancake.ToString();
        }
        else if(key == "pan")
        {
            icons[key].active = true;
        }
        else if (key == "bag")
        {
            icons[key].active = true;
        }
    }
    public void removeItem(string key)
    {

    }
    public int getCountKey(List<Item>items,string key)
    {
        int count = 0;
        for(int i = 0;i< items.Count; i++)
        {
            if (items[i].key == key) count++;
        }
        return count;
    }
}

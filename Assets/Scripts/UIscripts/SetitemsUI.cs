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

    private GetItem getItem;
    private void Awake()
    {
        // Создание инвенторя
        keyItems = PlayerData.getListKey;
        iconsitems = GetComponent<UIGameObjectHandler>().gameObjects;

        for (int i = 0; i < (iconsitems.Count - 2); i++)
        {
            icons.Add(keys[i], iconsitems[i]);
        }
        for(int i = (iconsitems.Count - 2); i < iconsitems.Count; i++)
        {

            countIcons.Add(keys[i], iconsitems[i]);
        }
        
        getItem = GetComponentInChildren<GetItem>();
    }
    private void Start()
    {
        // заполнение инвенторя
        for (int i = 0; i < keyItems.Count; i++)
        {
            if (icons[keyItems[i].key])
            {
                icons[keyItems[i].key].active = true;
            }
        }
        countPancake = PlayerData.GetCount("pancake");
        countKeys = PlayerData.GetCount("key");

        if (countPancake > 0) // Если панкейки есть, то включаем отображение их количества
        {
            countIcons["pancake"].SetActive(true);
            countIcons["pancake"].GetComponent<TextMeshProUGUI>().text = countPancake.ToString();
        }
        if (countKeys > 0) // Если ключи есть, то включаем отображение их количества
        {
            countIcons["key"].SetActive(true);
            countIcons["key"].GetComponent<TextMeshProUGUI>().text = countKeys.ToString();
        }
        //countPancake =  getCountKey(keyItems,"pancake");
        //countKeys = getCountKey(keyItems,"key");
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
            countIcons[key].active = true;
            countIcons[key].GetComponent<TextMeshProUGUI>().text = countPancake.ToString();
        }
        else if (key == "key")
        {
            if (countKeys == 0)
            {
                icons[key].active = true;
            }
            countKeys++;
            countIcons[key].active = true;
            countIcons[key].GetComponent<TextMeshProUGUI>().text = countKeys.ToString();
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
    public bool subtractItem(string key)
    {

        if (key == "pancake")
        {
            if (countPancake == 0) return false; // Оперцация не успешна, т.к. нету предметов которые можно было бы взять
            countPancake--;
            PlayerData.RemoveItem(key);
            if (countPancake == 0) { removeItem(key); return true; } // Операция успешна, удалён последний предмет

            if (countPancake > 0)
            {
                countIcons[key].GetComponent<TextMeshProUGUI>().text = countPancake.ToString();
                //PlayerData.getItem(key).count -= 1;
            }
            return true;
        }
        else if (key == "key")
        {
            if (countKeys == 0) return false; // Оперцация не успешна, т.к. нету предметов которые можно было бы взять
            countKeys--;
            PlayerData.RemoveItem(key);
            if (countKeys == 0){ removeItem(key); return true;} // Операция успешна, удалён последний предмет
            if(countKeys > 0)
            {
                countIcons[key].GetComponent<TextMeshProUGUI>().text = countKeys.ToString();
                //PlayerData.getItem(key).count -= 1;
            }
            return true;
        }
        return false; // Операция неуспешна
    }
    public void removeItem(string key)
    {

        if (key == "pancake")
        {
            icons[key].active = false;
            countIcons[key].active = false;
            getItem.clearItems();
        }
        else if (key == "key")
        {
            icons[key].active = false;
            countIcons[key].active = false;
            getItem.clearItems();
            //countIcons[key].GetComponent<TextMeshProUGUI>().text = countKeys.ToString();
        }
        
    }
    

    
}

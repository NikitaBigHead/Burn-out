using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvenoryDebug : MonoBehaviour
{
    private void Start()
    {
#if DEBUG
        this.gameObject.SetActive(true);
        //if (SceneManager.S)

#else
        this.gameObject.SetActive(false);
#endif
    }

    [SerializeField]
    private GameObject debugItemsUI;

    public void ShowInventory()
    {
        Debug.Log("Items in inventory:");
        foreach (Item item in PlayerData.getListKey)
        {
            Debug.Log(item.ToString());
        }
    }

    public void ShowCheckpoint()
    {
        Debug.Log("Items in checkpoint save:");
        foreach (Item item in CheckpointManager.savedItems)
        {
            Debug.Log(item.ToString());
        }
    }

    public void AddKeys()
    {
        Item item = new Item("key");
        PlayerData.AddItem("key");
        //PlayerData.listKey = item;
        GetComponentInParent<SetitemsUI>().addItem(ref item);
    }

    public void AddPan()
    {
        Item item = new Item("pan");
        PlayerData.AddItem("pan");
        //PlayerData.listKey = item;
        GetComponentInParent<SetitemsUI>().addItem(ref item);
    }

    public void AddPancake()
    {
        Item item = new Item("pancake");
        PlayerData.AddItem("pancake");
        //PlayerData.listKey = item;
        GetComponentInParent<SetitemsUI>().addItem(ref item);
    }

    public void AddBag()
    {
        Item item = new Item("bag");
        PlayerData.AddItem("bag");
        //PlayerData.listKey = item;
        GetComponentInParent<SetitemsUI>().addItem(ref item);
    }

    public void ShowOrHideDebugMenu()
    {
        debugItemsUI.SetActive(!debugItemsUI.activeInHierarchy);
    }
}

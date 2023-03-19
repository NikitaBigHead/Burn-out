using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenoryDebug : MonoBehaviour
{
    private void Start()
    {
#if DEBUG
        this.gameObject.SetActive(true);
#else
        this.gameObject.SetActive(false);
#endif
    }

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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemEdited : TakeItem
{
    public delegate void Acton(GameObject sender);
    public Acton OnPickUp;

    private new void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            PlayerData.PickUpPickUpItem(this);
            Item item = new Item(keyItem);
            //PlayerData.listKey = item;
            PlayerData.AddItem(keyItem);
            if (setItem != null) setItem.addItem(ref item);
            OnPickUp(this.gameObject);
            Destroy(this.gameObject);

        }
    }
}

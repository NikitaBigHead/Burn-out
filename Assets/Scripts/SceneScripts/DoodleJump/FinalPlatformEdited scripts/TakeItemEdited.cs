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
            Item item = new Item(keyItem);
            PlayerData.listKey = item;
            if (setItem != null) setItem.addItem(ref item);
            OnPickUp(this.gameObject);
            Destroy(this.gameObject);

        }
    }
}

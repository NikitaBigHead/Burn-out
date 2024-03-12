using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_ItemResource : DLC_Item
{
    private DLC_ItemInfo info;

    public string Name { get => info.Name; }
    public Sprite Icon { get => info.Icon; }
}
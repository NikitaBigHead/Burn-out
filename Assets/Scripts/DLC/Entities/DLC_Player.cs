using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_Player : DLC_Entity
{
    public override object GetData()
    {
        return new NotImplementedException();
    }

    public override void LoadData(object data)
    {
        DLC_PlayerData result = (DLC_PlayerData)data;
    }
}

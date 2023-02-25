using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData 
{
    private static GameObject prefabName;

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
}

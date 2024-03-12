using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameObjectList", menuName = "DLC_ScriptObj/New GameObjectList")]
public class DLC_GameObjectList : ScriptableObject
{
    [SerializeField]
    private GameObject[] objects = new GameObject[] { };
    
    public GameObject this[int i]
    {
        get { return objects[i]; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DLC_Entity : MonoBehaviour
{
    public abstract void LoadData(object data);
    public abstract object GetData();
}

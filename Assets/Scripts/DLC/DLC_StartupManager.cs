using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_StartupManager : MonoBehaviour
{
    private void Awake()
    {
        DLC_StaticData.Init("DLC_ch1");
    }
}

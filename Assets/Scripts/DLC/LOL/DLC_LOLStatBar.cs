using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLStatBar : MonoBehaviour
{
    [SerializeField]
    private Transform fillerAnchor;

    public void SetSize(float newSize)
    {
        fillerAnchor.localScale = new Vector3(newSize, 1, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLHealthBar : MonoBehaviour
{
    private Material material;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Health", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = (new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0) + new Vector3(-0.5f, -0.5f, -3f)) * 3f;
    }
}

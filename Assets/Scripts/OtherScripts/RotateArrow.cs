using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{

    public float speed = 6f;

    void Update()
    {
        this.transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}

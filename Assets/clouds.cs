using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clouds : MonoBehaviour
{
    public float speed = 10f;
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position. x + speed * Time.deltaTime,transform.position.y,1);
    }
}

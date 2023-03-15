using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanOffseter : MonoBehaviour
{
    public float offset = 0.58f;

    private Transform playerCords;
    private void Awake()
    {
        playerCords = transform.parent.transform;
    }

    void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerCords.position;
        if (dir.x >= 0)
        {
            transform.localPosition = new Vector3( offset,transform.localPosition.y,transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(-offset, transform.localPosition.y, transform.localPosition.z);
        }
    }
}

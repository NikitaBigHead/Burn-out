using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_coordinateAdjustiment : MonoBehaviour
{
    void LateUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
    }
}

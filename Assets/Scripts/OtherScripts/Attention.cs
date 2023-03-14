using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour
{
    private float interval;
    public float Interval
    {
        set { interval = value; }
    }
    public void StartAttention()
    {
        Invoke("stop", interval);
    }
    private void stop()
    {
        Destroy(this.gameObject);
    }
}

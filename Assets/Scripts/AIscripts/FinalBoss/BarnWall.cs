using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnWall : MonoBehaviour
{
    public int max = 10;
    private int current = 0;

    private void Start()
    {
        current = 0;
    }

    public void ReceiveDamage(float value)
    {
        current++;
        if (current >= max) OnDeath();
    }

    public void OnDeath()
    {
        Debug.Log("Wall died");
    }
}

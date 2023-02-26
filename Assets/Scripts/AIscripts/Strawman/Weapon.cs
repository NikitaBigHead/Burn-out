using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float attackDelay = 1.5f;

    public virtual float PerformAttack(Vector2 direction)
    {
        return attackDelay;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamageReceive : MonoBehaviour
{
    public virtual void OnHit(float invincibilityDelay) { }
    public virtual void Stop() { }
}

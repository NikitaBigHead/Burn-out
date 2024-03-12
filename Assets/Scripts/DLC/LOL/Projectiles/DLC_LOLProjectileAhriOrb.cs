using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLProjectileAhriOrb : DLC_LOLProjectile
{
    [SerializeField]
    private DLC_LOLProjectileHoming reverseMode;

    protected override void OnReachMaxRange()
    {
        this.enabled = false;
        reverseMode.enabled = true;
    }
}


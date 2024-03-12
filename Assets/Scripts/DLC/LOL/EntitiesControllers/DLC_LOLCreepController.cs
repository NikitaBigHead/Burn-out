using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DLC_LOLCreepController : DLC_LOLEntityController
{
    public override DLC_LOLEntityType entityType { get => DLC_LOLEntityType.Creep; }

    public int goldReward;
    public int xpReward;

    private void Awake()
    {
        EventOnDeath += Reward;
    }

    private void Reward()
    {
        DLC_LOLEntityController killer = receivedDamageFrom.OrderByDescending(keyValue => keyValue.Value).First().Key;
        if (killer != null && killer.entityType == DLC_LOLEntityType.Hero) 
        {
            (killer as DLC_LOLHeroController).AddGold(goldReward);
        }
    }
}

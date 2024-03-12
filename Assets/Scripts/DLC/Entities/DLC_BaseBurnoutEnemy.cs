using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DLC_BaseBurnoutEnemyData
{
    public float health;
}

public class DLC_BaseBurnoutEnemy : DLC_Enemy
{
    public override object GetData()
    {
        return new DLC_BaseBurnoutEnemyData { health = GetComponent<AttackableEntity>().health };
    }

    public override void LoadData(object data)
    {
        DLC_BaseBurnoutEnemyData result = (DLC_BaseBurnoutEnemyData)data;
        GetComponent<AttackableEntity>().health = result.health;
    }
}

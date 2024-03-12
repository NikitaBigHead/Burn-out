using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLEntityController : MonoBehaviour
{
    public DLC_LOLEntityStats stats;
    public virtual DLC_LOLEntityType entityType { get => DLC_LOLEntityType.Default; }
    public int team;

    public Dictionary<DLC_LOLEntityController, float> receivedDamageFrom = new();

    public delegate void EventFloat(float value);
    public delegate void Event();
    public event EventFloat EventOnReceiveDamage;
    public event EventFloat EventOnReceiveHeal;
    public event Event EventOnDeath;

    public void ReceiveDamage(float value, DLC_LOLDamageType damageType, DLC_LOLEntityController source)
    {
        switch (damageType)
        {
            case DLC_LOLDamageType.Physical:
                value = DamageMitigation(value, stats.armor);
                break;
            case DLC_LOLDamageType.Magic:
                value = DamageMitigation(value, stats.magicResist);
                break;
        }
        //Debug.Log($"received {value} damage of type {damageType.ToString()}");
        if ((stats.health -= value) <= 0) 
        {
            OnDeath();
        }
        if (receivedDamageFrom.ContainsKey(source))
        {
            receivedDamageFrom[source] = Time.timeSinceLevelLoad;
        }
        else
        {
            receivedDamageFrom.Add(source, Time.timeSinceLevelLoad);
        }
        EventOnReceiveDamage?.Invoke(value);
    }

    public void ReceiveHeal(float value, DLC_LOLHealthType healthType)
    {
        if ((stats.health += value) > stats.maxHealth)
        {
            stats.health = stats.maxHealth;
        }
        EventOnReceiveHeal?.Invoke(value);
    }

    public virtual void OnDeath()
    {
        EventOnDeath?.Invoke();
        Destroy(gameObject);
        Debug.Log($"{name} погиб.");
    }

    public static float DamageMitigation(float damage, float resist)
    {
        if (resist < 0) return damage * (2 - 100 / (100 - resist));
        return damage * 100 / (100 + resist);
    }
}

public enum DLC_LOLEntityType
{
    Default,
    Creep,
    Monster,
    Hero,
    Building
}

public enum DLC_LOLDamageType
{
    Physical,
    Magic,
    True,
    HealthRemoval
}

public enum DLC_LOLHealthType
{
    Regeneration,
    Vampiric,
    HealthAddition
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DLC_LOLSkill : MonoBehaviour
{
    public int slot;

    public string skillName;
    public int level;
    public float cost;
    public float cooldown;
    public float castTime;
    public Sprite icon;

    public DLC_LOLEntityController controller;

    public abstract string Description();
    public abstract void Action();
    public abstract void Upgrade();
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DLC_LOLHeroController : DLC_LOLEntityController
{
    public override DLC_LOLEntityType entityType { get => DLC_LOLEntityType.Hero; }

    public int gold;
    public event EventFloat EventOnReceiveGold;

    private DLC_LOLSkill[] skills;

    public void AddGold(int amount)
    {
        gold += amount;
        EventOnReceiveGold?.Invoke(amount);
    }

    private void Start()
    {
        skills = (from skill in GetComponents<DLC_LOLSkill>()
                                         orderby skill.slot
                                         select skill).ToArray();
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].slot = i;
            skills[i].controller = this;
        }
    }

    public void UseSkill1()
    {
        skills[0].Action();
    }
}

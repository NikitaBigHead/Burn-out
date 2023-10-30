using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private ShowStatsUI statsUI;

    public List<Stat> stats = new List<Stat>();
    public Dictionary<int, int> statsValues = new Dictionary<int, int>();

    public int level = 0;
    public int xp = 0;
    public int xpToNextLvl = 1;
    public int skillPoints = 0;

    public void AddXp(int value)
    {
        xp += value;
        while (xp > xpToNextLvl)
        {
            xp -= xpToNextLvl;
            level++;
            skillPoints++;
            xpToNextLvl += 1;
        } 
    }

    private void Start()
    {
        if (statsUI == null)
        {
            statsUI = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<ShowStatsUI>();
        }
        foreach (Stat stat in stats)
        {
            statsValues.Add(stat.statId, 0);
            statsUI.AddDisplayStat(stat, 0);
        }
    }
}

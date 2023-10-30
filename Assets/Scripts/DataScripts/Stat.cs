using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatNew", menuName = "Stat/New Stat")]
public class Stat : ScriptableObject
{
    public int statId;
    public string statName;
}

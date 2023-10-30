using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StatList", menuName = "Stat/New StatList")]
public class StatList : ScriptableObject
{
    public List<Stat> statList;
}

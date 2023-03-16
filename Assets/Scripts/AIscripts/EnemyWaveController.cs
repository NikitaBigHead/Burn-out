using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public int enemyCount = 0;

    public delegate void Event(GameObject sender);
    List<Event> onEnd = new List<Event>();

    private void Awake()
    {
        if (PlayerData.villageBrawlComplited) Destroy(this.gameObject);
        foreach (AttackableEntity enemy in GetComponentsInChildren<AttackableEntity>())
        {
            enemy.actionsOnDeaths.Add(WhenEnemyDies);
            enemyCount++;
        }
    }

    public void WhenEnemyDies(GameObject sender)
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            End();
        }
    }

    public virtual void Run()
    {

    }

    public virtual void End()
    {
        foreach (Event ev in onEnd)
        {
            ev(this.gameObject);
        }
    }
}

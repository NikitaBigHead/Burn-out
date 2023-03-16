using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableFinalBoss2 : AttackableEntity
{
    [SerializeField]
    private FinalBossAiPhase2 bossAi;

    protected new void Start()
    {
        if (bossAi == null) bossAi = GetComponent<FinalBossAiPhase2>();
        onDamageReceive = gameObject.GetComponent<OnDamageReceive>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<Blinking>();
        actionsOnDeaths.Add(bossAi.OnDeath);
    }

    public override void RecieveDamage(float value)
    {
        if (!invincible)
        {
            this.invincible = true;
            onDamageReceive.OnHit(health);
            health -= value;
            if (health <= 0) OnDeath();
            Invoke("InvincibilityEnd", invincibilityDelay);
        }
    }

    protected override void InvincibilityEnd()
    {
        invincible = false;
        onDamageReceive.Stop();
    }
}

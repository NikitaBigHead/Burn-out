using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableFinalBoss1 : AttackableEntity
{
    [SerializeField]
    private GameObject phase1;

    [SerializeField]
    private GameObject phase2;

    protected new void Start()
    {
        onDamageReceive = gameObject.GetComponent<OnDamageReceive>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<Blinking>();
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

    protected override void OnDeath()
    {
        this.health = 100;
        phase1.SetActive(false);
        phase2.SetActive(true);
    }
}

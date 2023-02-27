using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackablePlayer : AttackableEntity
{
    protected new void Start()
    {
        onDamageReceive = gameObject.GetComponent<PlayerOnHit>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<PlayerOnHit>();
    }

    public override void RecieveDamage(float value)
    {
        if (!invincible)
        {
            this.invincible = true;
            ((PlayerOnHit)onDamageReceive).OnHit(health, 100f);
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

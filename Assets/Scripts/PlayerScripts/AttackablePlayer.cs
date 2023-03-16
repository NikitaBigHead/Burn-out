using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackablePlayer : AttackableEntity
{
    public float maxHealth = 100f;

    protected new void Start()
    {
        onDamageReceive = gameObject.GetComponent<PlayerOnHit>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<PlayerOnHit>();
        actionsOnDeaths.Add(CheckpointManager.LoadCheckpoint);
    }

    public override void RecieveDamage(float value)
    {
        if (!invincible)
        {
            this.invincible = true;
            ((PlayerOnHit)onDamageReceive).OnHit(health, maxHealth);
            health -= value;   
            if (health <= 0) OnDeath();
            PlayerData.playerCurrentHealth = health;
            Invoke("InvincibilityEnd", invincibilityDelay);
        }
    }

    public override void RecieveHeal(float value)
    {
        health += value;
        if (health > maxHealth) health = maxHealth;
    }

    protected override void InvincibilityEnd()
    {
        invincible = false;
        onDamageReceive.Stop();
    }
}

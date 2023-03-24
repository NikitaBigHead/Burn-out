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
            health -= value;
            PlayerData.playerCurrentHealth = health;
            ((PlayerOnHit)onDamageReceive).OnHit(health, maxHealth);
            if (health <= 0) OnDeath();
            Invoke("InvincibilityEnd", invincibilityDelay);
        }
    }

    public override void RecieveHeal(float value)
    {
        this.invincible = true;
        health += value;
        if (health > maxHealth) health = maxHealth;
        PlayerData.playerCurrentHealth = health;
        ((PlayerOnHit)onDamageReceive).OnHit(health, maxHealth);
        Invoke("InvincibilityEnd", invincibilityDelay);
    }

    protected override void InvincibilityEnd()
    {
        invincible = false;
        onDamageReceive.Stop();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableFinalBoss1 : AttackableEntity
{
    [SerializeField]
    private GameObject phase1;

    [SerializeField]
    private GameObject phase2;

    [SerializeField]
    private Transform player;

    protected new void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        onDamageReceive = gameObject.GetComponent<OnDamageReceive>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<Blinking>();
        actionsOnDeaths.Add(WhenDeath);
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

    protected static void WhenDeath(GameObject sender)
    {
        AttackableFinalBoss1 attackableFinalBoss1 = sender.GetComponent<AttackableFinalBoss1>();
        attackableFinalBoss1.health = 100;
        Camera.main.transform.SetParent(attackableFinalBoss1.player.transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        attackableFinalBoss1.phase1.SetActive(false);
        attackableFinalBoss1.phase2.SetActive(true);
    }
}

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
        Camera.main.transform.SetParent(player.transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        phase1.SetActive(false);
        phase2.SetActive(true);
    }
}

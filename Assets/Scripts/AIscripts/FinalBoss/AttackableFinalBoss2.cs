using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableFinalBoss2 : AttackableEntity
{
    [SerializeField]
    private FinalBossAiPhase2 bossAi;

    [SerializeField]
    private Canvas gameUI;

    [SerializeField]
    private GameObject healthBarPrefab;

    private HealthBar healthBar;

    protected new void Start()
    {
        if (bossAi == null) bossAi = GetComponent<FinalBossAiPhase2>();
        onDamageReceive = gameObject.GetComponent<OnDamageReceive>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<Blinking>();
        actionsOnDeaths.Add((GameObject sender) => { Destroy(sender.GetComponent<AttackableFinalBoss2>().healthBar.gameObject); });
        actionsOnDeaths.Add(bossAi.OnDeath);

        if (gameUI == null)
        {
            gameUI = GameObject.Find("UI").GetComponent<Canvas>();
        }
        healthBar = Instantiate(healthBarPrefab, gameUI.transform).GetComponent<HealthBar>();

        healthBar.currentHealth = health;
        healthBar.maxHealth = health;
        healthBar.SetCurrentHealth(health);
    }

    public override void RecieveDamage(float value)
    {
        if (!invincible)
        {
            this.invincible = true;
            onDamageReceive.OnHit(health);
            health -= value;
            healthBar.RecieveDamage(value);
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

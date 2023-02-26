using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEntity : MonoBehaviour
{
    public float health = 100f;
    public float invincibilityDelay = 0.25f;

    private bool invincible = false;

    private OnDamageReceive onDamageReceive;

    private void Start()
    {
        onDamageReceive = gameObject.GetComponent<OnDamageReceive>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<OnDamageReceive>();
    }

    public void RecieveDamage(float value)
    {
        if (!invincible)
        {
            this.invincible = true;
            onDamageReceive.OnHit(invincibilityDelay);
            health -= value;
            if (health <= 0) OnDie();
            Invoke("InvincibilityEnd", invincibilityDelay);
        }
    }

    public void RecieveHeal(float value)
    {
        health += value;
    }

    protected void OnDie()
    {
        Destroy(this.gameObject);
    }

    void InvincibilityEnd()
    {
        invincible = false;
        onDamageReceive.Stop();
    }
}

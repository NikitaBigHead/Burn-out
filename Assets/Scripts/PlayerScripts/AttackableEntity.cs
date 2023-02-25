using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEntity : MonoBehaviour
{
    public float health = 100f;
    public float invincibilityDelay = 0.25f;

    private bool invincible = false;

    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!invincible)
        {
            DamageSource damageSource = collision.gameObject.GetComponent<DamageSource>();
            if (damageSource != null)
            {
                RecieveDamage(damageSource.damage);
            }
        }
    }
    */
    public void RecieveDamage(float value)
    {
        health -= value;
        if (health <= 0) OnDie();
        this.invincible = true;
        Invoke("InvincibilityEnd", invincibilityDelay);
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
    }
}

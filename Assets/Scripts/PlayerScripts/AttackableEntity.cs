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
    public void RecieveImpulse(float range, float speed, Vector2 direction) {
        StartCoroutine(Flight(range,  speed,  direction,5));
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

    IEnumerator Flight(float range, float speed, Vector2 direction,float timeMotionLess)
    {
        List<MonoBehaviour> scripts = gameObject.GetComponent<ScriptsHolder>().list;

        float dist = 0;
        for (int i = 0; i < scripts.Count; i++)
        {
            scripts[i].enabled = false;
        }


        while (dist <= range)
        {
            Vector2 translation = new Vector2(direction.x, direction.y) * speed;
            transform.Translate(translation);
            dist += translation.magnitude;
            yield return new WaitForFixedUpdate();
        }
        gameObject.GetComponent<NavMeshholder>().navMesh.active = false;
        StartCoroutine(motionLess(timeMotionLess));

    }

    IEnumerator motionLess(float timeMotionLess) {
        float time = 0;
        List<MonoBehaviour> scripts = gameObject.GetComponent<ScriptsHolder>().list;
        
        while (time <= timeMotionLess)
        {
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        gameObject.GetComponent<NavMeshholder>().navMesh.active = true;
        for (int i = 0; i < scripts.Count; i++)
        {
            scripts[i].enabled = true;
        }


    }

}

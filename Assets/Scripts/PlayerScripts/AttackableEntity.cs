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

    public void RecieveImpulse(float range, float speed, Vector2 direction)
    {
        StartCoroutine(Flight(range, speed, direction, 5));
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

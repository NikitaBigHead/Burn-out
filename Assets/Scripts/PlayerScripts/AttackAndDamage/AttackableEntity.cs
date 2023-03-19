using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackableEntity : MonoBehaviour
{
    public float health = 100f;
    public float invincibilityDelay = 0.25f;

    protected bool invincible = false;

    [SerializeField]
    protected OnDamageReceive onDamageReceive;
    private NavMeshAgent navMeshAgent;

    public delegate void ActionOnDeath(GameObject sender);
    //public Dictionary<int, ActionOnDeath> actionsOnDeaths = new Dictionary<int, ActionOnDeath>();
    public List<ActionOnDeath> actionsOnDeaths = new List<ActionOnDeath>();

    /// <summary>
    /// Добавление события вызываемого при смерти
    /// </summary>
    /// <param name="action">Событие длжно принимать единственный аргумент GameObject</param>
    /// <param name="prioritet">Приоритет вызова, чем больше число, тем раньше вызовется метод</param>
    public void AddActionOnDeath(ActionOnDeath action, int prioritet)
    {
        actionsOnDeaths.Add(action);
    }

    //public void InsertActionOnDeath

    protected void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        onDamageReceive = gameObject.GetComponent<OnDamageReceive>();
        if (onDamageReceive == null) onDamageReceive = gameObject.AddComponent<OnDamageReceive>();

        // Добовляем метод удаления в список методов, вызываемых при смерти
        actionsOnDeaths.Add(Destroy);
    }

    public virtual void RecieveDamage(float value)
    {
        if (!invincible)
        {
            this.invincible = true;
            onDamageReceive.OnHit(invincibilityDelay);
            health -= value;
            if (health <= 0) OnDeath();
            Invoke("InvincibilityEnd", invincibilityDelay);
        }
    }

    public void RecieveImpulse(float range, float speed, Vector2 direction)
    {
        if(range!=0 && speed!=0) { StartCoroutine(Flight(range, speed, direction, 5)); }

    }

    public virtual void RecieveHeal(float value)
    {
        health += value;
    }

    protected virtual void OnDeath()
    {
        foreach (ActionOnDeath actionOnDeath in actionsOnDeaths)
            actionOnDeath(this.gameObject);
    }

    protected virtual void InvincibilityEnd()
    {
        invincible = false;
        onDamageReceive.Stop();
    }

    protected IEnumerator Flight(float range, float speed, Vector2 direction,float timeMotionLess)
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
        //gameObject.GetComponent<NavMeshholder>().navMesh.active = false;
        navMeshAgent.enabled = false;
        StartCoroutine(motionLess(timeMotionLess));

    }

    protected IEnumerator motionLess(float timeMotionLess) {
        float time = 0;
        List<MonoBehaviour> scripts = gameObject.GetComponent<ScriptsHolder>().list;
        
        while (time <= timeMotionLess)
        {
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        //gameObject.GetComponent<NavMeshholder>().navMesh.active = true;
        navMeshAgent.enabled = true;
        for (int i = 0; i < scripts.Count; i++)
        {
            scripts[i].enabled = true;
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWave : MonoBehaviour
{
    [SerializeField]
    private GameObject mask;
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private GameObject hitboxSafety;
    [SerializeField]
    private AttackableEntity target;

    public float damage = 5;

    public float waveThickness = 0.65f;
    public float safetyOffset = 0.65f;

    public float speed = 0f;
    public float maxRange = 10f;

    private float currentRange = 1f;

    private bool inSafe = false;
    private bool inHarm = false;

    private void Awake()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackableEntity>();

        HitboxController harm = sprite.GetComponent<HitboxController>();
        harm.OnEnterCollision = HarmTriggerEnter;
        harm.OnExitCollision = HarmTriggerExit;

        HitboxController safety = hitboxSafety.GetComponent<HitboxController>();
        safety.OnEnterCollision = SafetyTriggerEnter;
        safety.OnExitCollision = SafetyTriggerExit;
    }

    public void Launch(float damage, float waveThickness, float safetyOffset, float speed, float maxRange)
    {
        this.waveThickness = waveThickness;
        this.speed = speed;
        this.maxRange = maxRange;
        this.damage = damage;
        this.safetyOffset = safetyOffset;
        hitboxSafety.transform.localScale = new Vector3(currentRange - waveThickness - safetyOffset, currentRange - waveThickness - safetyOffset, 1);
        mask.transform.localScale = new Vector3(currentRange - waveThickness, currentRange - waveThickness, 1);
        sprite.transform.localScale = new Vector3(currentRange, currentRange, 1);
    }

    public void FixedUpdate()
    {
        currentRange += speed * Time.deltaTime;
        float hitboxSafetySize = currentRange - waveThickness - safetyOffset;
        if (hitboxSafetySize < 0) hitboxSafetySize = 0;
        hitboxSafety.transform.localScale = new Vector3(hitboxSafetySize, hitboxSafetySize, 1);
        mask.transform.localScale = new Vector3(currentRange - waveThickness, currentRange - waveThickness, 1);
        sprite.transform.localScale = new Vector3(currentRange, currentRange, 1);
        if (currentRange > maxRange) Destroy(this.gameObject);
        if (inHarm && !inSafe)
        {
            target.RecieveDamage(damage);
        }
    }

    void SafetyTriggerEnter(GameObject sender, GameObject collider)
    {
        if (collider.tag == "Player")
        {
            inSafe = true;
        }
    }

    void SafetyTriggerExit(GameObject sender, GameObject collider)
    {
        if (collider.tag == "Player")
        {
            inSafe = false;
        }
    }

    void HarmTriggerEnter(GameObject sender, GameObject collider)
    {
        if (collider.tag == "Player")
        {
            inHarm = true;
        }
    }

    void HarmTriggerExit(GameObject sender, GameObject collider)
    {
        if (collider.tag == "Player")
        {
            inHarm = false;
        }
    }
}

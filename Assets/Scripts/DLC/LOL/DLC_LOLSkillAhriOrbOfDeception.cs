using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLSkillAhriOrbOfDeception : DLC_LOLSkill
{
    [SerializeField]
    private GameObject orbPrefab;

    public float baseDamage;
    public float abilityPowerScale;
    public float range;

    [Header("Туда")]
    public float speed = 12.05555f;
    public float maxSpeed = 12.05555f;
    public float acceleration = 0;

    [Header("Обратно")]
    public float backSpeed = 0.46666f;
    public float backMaxSpeed = 20.22222f;
    public float backAcceleration = 14.77778f;

    public override void Action()
    {
        GameObject orbInstance = Instantiate(orbPrefab, this.transform.position, Quaternion.identity);
        var orbToSettings = orbInstance.GetComponent<DLC_LOLProjectileAhriOrb>();
        var orbBackSettings = orbInstance.GetComponent<DLC_LOLProjectileHoming>();

        orbToSettings.source = controller;
        orbToSettings.damage = baseDamage + abilityPowerScale * controller.stats.abilityPower;
        orbToSettings.acceleration = acceleration;
        orbToSettings.damageType = DLC_LOLDamageType.Magic;
        orbToSettings.range = range;
        orbToSettings.speed = speed;
        orbToSettings.maxSpeed = speed;

        orbToSettings.direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        orbToSettings.direction = orbToSettings.direction.normalized;

        orbBackSettings.source = controller;
        orbBackSettings.damage = baseDamage + abilityPowerScale * controller.stats.abilityPower;
        orbBackSettings.acceleration = backAcceleration;
        orbBackSettings.damageType = DLC_LOLDamageType.True;
        orbBackSettings.speed = backSpeed;
        orbBackSettings.maxSpeed = maxSpeed;
        orbBackSettings.target = this.transform;

        orbInstance.SetActive(true);
    }

    public override string Description()
    {
        return $"deals {baseDamage + abilityPowerScale * controller.stats.abilityPower} magic damage ({baseDamage} + {abilityPowerScale * 100}% AP) in one way, and returns to Ahri dealing true damage";
    }

    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}

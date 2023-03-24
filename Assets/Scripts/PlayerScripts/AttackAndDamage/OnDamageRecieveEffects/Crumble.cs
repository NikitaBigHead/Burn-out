using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : OnDamageReceive
{
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private ParticleSystem defaultParticleSystemPrefab;

    private void Start()
    {
        if (particle == null)
            particle = GetComponentInChildren<ParticleSystem>();
        if (particle == null)
        {
            Instantiate(defaultParticleSystemPrefab, transform).transform.localPosition = new Vector3(0, 0, -1);
            particle = GetComponentInChildren<ParticleSystem>();
        }
    }

    public override void OnHit(float invincibilityDelay)
    {
        particle.Play();
    }

    public override void Stop()
    {
        particle.Stop();
    }
}

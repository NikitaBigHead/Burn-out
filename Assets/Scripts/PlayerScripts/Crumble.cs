using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : OnDamageReceive
{
    [SerializeField]
    private ParticleSystem particle;

    private void Start()
    {
        if (particle == null)
            particle = GetComponentInChildren<ParticleSystem>();
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

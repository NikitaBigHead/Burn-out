using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawmanOnHit : OnDamageReceive
{
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private ParticleSystem defaultParticleSystemPrefab;

    public float delay = 0.1f;
    public Color blinkColor = Color.gray;

    [SerializeField]
    private SpriteRenderer sprite;

    private bool blinkState = false;

    private IEnumerator coroutine;

    private void Start()
    {
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();
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
        blinkState = false;
        coroutine = Blink();
        StartCoroutine(coroutine);
    }

    public override void Stop()
    {
        particle.Stop();
        blinkState = false;
        sprite.color = Color.white;
        StopCoroutine(coroutine);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if (blinkState)
            {
                sprite.color = blinkColor;
            }
            else
            {
                sprite.color = Color.white;
            }
            blinkState = !blinkState;
            yield return new WaitForSeconds(delay);
        }
    }
}

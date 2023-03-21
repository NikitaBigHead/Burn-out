using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Blinking : OnDamageReceive
{
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
    }

    public override void OnHit(float invincibilityDelay) 
    {
        blinkState = false;
        coroutine = Blink();
        StartCoroutine(coroutine);  
    }

    public override void Stop()
    {
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

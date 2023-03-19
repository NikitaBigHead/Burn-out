using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerOnHit : OnDamageReceive
{
    public Vector3 offset = new Vector3(0, 2f, 0);
    public float delay = 0.1f;
    //public float fadeDelay = 0.01f;
    public Color blinkColor = Color.gray;
    private Color defaultColor;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private TextMeshProUGUI textHealth;

    private bool blinkState = false;

    private IEnumerator coroutine;
    private IEnumerator fadeCoroutine;

    private bool stay = false; 

    private void Start()
    {
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();
        defaultColor = sprite.color;
        if (Camera.main.transform.parent == transform)
        {
            stay = true;
            textHealth.rectTransform.localPosition = offset;
        }
    }

    public override void OnHit(float health)
    {
        textHealth.gameObject.SetActive(true);
        if (!stay)
        textHealth.rectTransform.localPosition = transform.position + offset;
        textHealth.text = $"{health}/100";
        blinkState = false;
        coroutine = Blink();
        StartCoroutine(coroutine);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        faded = 1f;
        fadeCoroutine = FadeText();
        timePassed = 0f;    
        StartCoroutine(fadeCoroutine);
    }

    public void OnHit(float health, float maxHealth)
    {
        textHealth.gameObject.SetActive(true);
        if (!stay)
            textHealth.rectTransform.localPosition = (stay ? offset : transform.position + offset);
        textHealth.text = $"{health}/{maxHealth}";
        blinkState = false;
        coroutine = Blink();
        StartCoroutine(coroutine);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        faded = 1f;
        fadeCoroutine = FadeText();
        timePassed = 0f;
        StartCoroutine(fadeCoroutine);
    }

    public override void Stop()
    {
        blinkState = false;
        sprite.color = defaultColor;
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
                sprite.color = defaultColor;
            }
            blinkState = !blinkState;
            yield return new WaitForSeconds(delay);
        }
    }

    protected float faded = 1f;
    protected float speedFade = 0.02f;
    float timePassed = 0f;
    float maxFadeTime = 1f;

    IEnumerator FadeText()
    {
        while (timePassed < maxFadeTime)
        {
            timePassed += Time.fixedDeltaTime;
            if (!stay)
                textHealth.rectTransform.localPosition = transform.position + offset;
            faded -= speedFade;
            textHealth.color = new Color(1, 1, 1, faded);
            yield return new WaitForFixedUpdate();
        }
        textHealth.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Color fullHealth = Color.green;
    public Color lowHealth = Color.red;

    [SerializeField]
    private Image background;
    [SerializeField]
    private Image healthIndicator;

    private void Awake()
    {
        if (background == null) background = GetComponentsInChildren<Image>()[0];
        if (healthIndicator == null) background = GetComponentsInChildren<Image>()[1];
    }

    public void RecieveDamage(float value)
    {
        float newHealth = currentHealth - value;
        if (newHealth < 0) newHealth = 0;
        SetCurrentHealth(newHealth);
    }

    public void SetCurrentHealth(float value)
    {
        currentHealth = value;
        float currentHealthNormalized = 1 / maxHealth * currentHealth;
        healthIndicator.fillAmount = currentHealthNormalized;
        healthIndicator.color = new Color(
            fullHealth.r * currentHealthNormalized + lowHealth.r * (1 - currentHealthNormalized),
            fullHealth.g * currentHealthNormalized + lowHealth.g * (1 - currentHealthNormalized),
            fullHealth.b * currentHealthNormalized + lowHealth.b * (1 - currentHealthNormalized));
    }
}

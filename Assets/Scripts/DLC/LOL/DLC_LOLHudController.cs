using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DLC_LOLHudController : MonoBehaviour
{
    private DLC_LOLEntityController entity;

    public void Select(DLC_LOLEntityController newEntity)
    {
        if (newEntity == null || newEntity == entity) return;
        if (entity != null)
        {
            entity.EventOnReceiveDamage -= ChangeHealth;
            entity.EventOnReceiveHeal -= ChangeHealth;
        }
        entity = newEntity;
        ChangeHealth(0);
        ChangeMana(0);
        ChangeGold(0);
        entity.EventOnReceiveDamage += ChangeHealth;
        entity.EventOnReceiveHeal += ChangeHealth;
        if (entity is DLC_LOLHeroController)
        {
            (entity as DLC_LOLHeroController).EventOnReceiveGold += ChangeGold;
        }
    }

    [SerializeField]
    private GameObject healthBar;
    private Material healthBarMaterial;
    private TMPro.TMP_Text healthBarText;

    [SerializeField]
    private GameObject manaBar;
    private Material manaBarMaterial;
    private TMPro.TMP_Text manaBarText;

    [SerializeField]
    private TMPro.TMP_Text goldText;

    private void ChangeHealth(float value)
    {
        healthBarMaterial.SetFloat("_Health", entity.stats.health / entity.stats.maxHealth);
        healthBarText.text = entity.stats.health.ToString("F0") + "/" + entity.stats.maxHealth.ToString("F0");
        if (value == 0) return;
        DLC_LOLFadingText text = Instantiate(DLC_LOLStaticData.damageNumbersPrefab, healthBar.transform).GetComponent<DLC_LOLFadingText>();
        text.text.text = value.ToString("F0");
        text.gameObject.SetActive(true);
    }

    private void ChangeMana(float value)
    {
        manaBarMaterial.SetFloat("_Health", entity.stats.mana / entity.stats.maxMana);
        manaBarText.text = entity.stats.mana.ToString("F0") + "/" + entity.stats.maxMana.ToString("F0");
        if (value == 0) return;
        DLC_LOLFadingText text = Instantiate(DLC_LOLStaticData.damageNumbersPrefab, manaBar.transform).GetComponent<DLC_LOLFadingText>();
        text.text.text = value.ToString("F0");
        text.gameObject.SetActive(true);
    }

    private void ChangeGold(float value)
    {
        if (entity is DLC_LOLHeroController)
        {
            goldText.text = ((DLC_LOLHeroController)entity).gold.ToString();
        } else
        {
            goldText.text = "0";
        }
    }

    private void Awake()
    {
        healthBarMaterial = healthBar.GetComponent<Image>().material;
        manaBarMaterial = manaBar.GetComponent<Image>().material;

        healthBarText = healthBar.GetComponentInChildren<TMPro.TMP_Text>(true);
        manaBarText = manaBar.GetComponentInChildren<TMPro.TMP_Text>(true);

        hudController = this;
    }

    public static DLC_LOLHudController hudController;
}

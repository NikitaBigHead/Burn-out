using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStatsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabStatUI;

    [SerializeField]
    private GameObject panel;

    public float offset = 5f;

    private float statPosY = 0;

    void Awake()
    {
        statPosY = -(prefabStatUI.GetComponent<RectTransform>().rect.height * 0.5f + offset);
    }

    public void AddDisplayStat(Stat stat, int level)
    {
        StatUI statUIObject = Instantiate(prefabStatUI, panel.transform).GetComponent<StatUI>();
        RectTransform rectTransform = statUIObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = new Vector3(0, statPosY, 0);
        statPosY -= (rectTransform.rect.height + offset);

        statUIObject.SetStat(stat.statName, level);
    }
}

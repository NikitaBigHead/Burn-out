using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text statNameText;

    [SerializeField]
    private TMP_Text statLevelText;

    [SerializeField]
    private Button statIncreaseButton;

    [SerializeField]
    private Button statDecreaseButton;

    public void SetStat(string name, int value)
    {
        statNameText.text = name;
        statLevelText.text = value.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Pancake : MonoBehaviour
{
    public int count = 0;
    private TextMeshProUGUI text;
    public TextMeshProUGUI Text
    {
        set
        {
            text = value;
        }
    }
    public void useKey()
    {
        count--;
        if (count == 0)
        {
            return;
        }
        text.text = count.ToString();

    }
}

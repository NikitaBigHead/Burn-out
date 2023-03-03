using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int count = 0;

    private TextMeshProUGUI text;

    public TextMeshProUGUI Text
    {
        set
        {
            text= value;
        }
    }
    public void eatPancake()
    {
        //anim
        count--;
        if (count == 0)
        {
            //destroy
            return;
        }
        text.text = count.ToString();

    }


}

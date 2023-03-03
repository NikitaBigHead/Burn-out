using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Pancake : MonoBehaviour
{
    private int count = 0;
    private TextMeshProUGUI text;
    private SetitemsUI setitems;
    public TextMeshProUGUI Text
    {
        set
        {
            text = value;
        }
    }
    public SetitemsUI SetItem
    {
        set
        {
            setitems = value;
        }
    }
    public void eatPancake()
    {
        //anim
        count--;
        if (count == 0)
        {
            gameObject.active = false;
            setitems.removeItem("pancake");
            return;
        }
        text.text = count.ToString();

    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            eatPancake();
        }
    }

}

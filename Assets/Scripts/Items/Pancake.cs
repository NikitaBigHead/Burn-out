using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Pancake : MonoBehaviour
{
  
    private SetitemsUI setitems;

   
    public SetitemsUI SetItem
    {
        set
        {
            setitems = value;
        }
    }
    private void Start()
    {
        Debug.Log(setitems);
    }
    public void eatPancake()
    {
        setitems.subtractItem("pancake");
        //anim
        //count--;
        //if (count == 0)
        //{
          //  //gameObject.active = false;
            //setitems.subtractItem("pancake");
            //return;
        //}
        //text.text = count.ToString();

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eatPancake();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{

    private SetitemsUI setitems;

    public SetitemsUI SetItem
    {
        set
        {
            setitems= value;
        }
    }
    public void useKey()
    {
        setitems.subtractItem("key");

    }


}

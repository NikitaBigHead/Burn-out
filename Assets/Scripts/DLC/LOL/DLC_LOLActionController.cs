using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLActionController : MonoBehaviour
{
    public delegate object Action(params object[] actionParams);

    Queue<Action> actions = new();

    void TryPerformAction(Action action)
    {
        actions.Enqueue(action);
    }

    void TryBufferAction(Action action)
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatformEditedController : MonoBehaviour
{
    [SerializeField] 
    private TakeItemEdited key;
    [SerializeField] 
    private AttackableEntity snowman;

    bool keyTaken = false;
    bool snowManDead = false;

    public void Awake()
    {
        key.OnPickUp = OnEvent;
        snowman.actionsOnDeaths.Add(OnEvent);
    }

    void OnEvent(GameObject sender)
    {
        if (sender.GetComponent<TakeItemEdited>() != null)
            keyTaken = true;
        if (sender.GetComponent<AttackableEntity>() != null)
            snowManDead = true;

        if (keyTaken && snowManDead)
        {
            SceneLoader.LoadScene("PostLocation");
        }
    }

}

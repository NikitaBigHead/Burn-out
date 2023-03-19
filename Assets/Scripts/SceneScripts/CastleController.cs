using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    [SerializeField]
    private GameObject keyPositon;


    void Start()
    {
        keyPositon.SetActive(false);
        if (!PlayerData.castleKeyRecieved)
        {
            keyPositon.GetComponentInChildren<TakeItemEdited>().OnPickUp = (GameObject sender) => 
            { 
                PlayerData.castleKeyRecieved = true; 
            };
            keyPositon.SetActive(true);
        }
    }

}

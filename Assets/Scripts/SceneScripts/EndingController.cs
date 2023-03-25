using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    [SerializeField]
    private GameObject endingUI;

    [SerializeField]
    private GameObject player;

    public void Start()
    {
        endingUI.SetActive(false);
    }

    public void EndGame()
    {
        Camera.main.transform.SetParent(null);
        endingUI.SetActive(true);
        player.SetActive(false);
    }
}

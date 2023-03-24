using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Pancake : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audio;

    public int heal = 10;
    private SetitemsUI setitems;
    private AttackableEntity attackableEntity;
   
    public SetitemsUI SetItem
    {
        set
        {
            setitems = value;
        }
    }
    private void Awake()
    {
        attackableEntity = GameObject.Find("Player").GetComponent<AttackableEntity>();
        audioSource = transform.parent.GetComponent<AudioSource>();
    }

    public void eatPancake()
    {
        if (setitems.subtractItem("pancake"))
        {
            audioSource.PlayOneShot(audio);

            attackableEntity.RecieveHeal(10);
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eatPancake();
        }
    }

}

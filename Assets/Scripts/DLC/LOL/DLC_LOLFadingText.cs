using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLFadingText : MonoBehaviour
{
    [SerializeField]
    public TMPro.TMP_Text text;

    public float time = 1f;

    private void Awake()
    {
        if (text == null) text = GetComponent<TMPro.TMP_Text>();
    }

    public void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

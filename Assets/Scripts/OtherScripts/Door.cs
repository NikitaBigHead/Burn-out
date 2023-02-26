using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string Scene;

    public bool open = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (open)
            SceneManager.LoadScene(Scene);
    }
}

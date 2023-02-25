using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string Scene;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(Scene);
    }
}

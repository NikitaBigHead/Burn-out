using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private CutsceneScript cutsceneController;

    public void OnButtonStart()
    {
        cutsceneController.StartCutscene();
    }

    public void OnButtonExit()
    {
        Application.Quit();
    }
}

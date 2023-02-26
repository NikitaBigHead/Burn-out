using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();

    [SerializeField]
    private GameObject cutsceneUI;

    [SerializeField]
    private Image background;

    private int currentSpriteIndex = 0;

    public void StartCutscene()
    {
        if (StateController.cutsceneComplited)
            SceneManager.LoadScene("StartLocation", LoadSceneMode.Single);
        background.sprite = sprites[currentSpriteIndex];
        cutsceneUI.SetActive(true);
    }

    public void NextImage()
    {
        currentSpriteIndex++;
        if (currentSpriteIndex == sprites.Count)
        {
            StateController.cutsceneComplited = true;
            cutsceneUI.SetActive(false);
            SceneManager.LoadScene("StartLocation", LoadSceneMode.Single);
        }
        else
        {
            background.sprite = sprites[currentSpriteIndex];
        }
    }
}

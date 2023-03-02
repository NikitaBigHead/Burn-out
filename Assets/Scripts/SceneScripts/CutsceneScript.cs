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
        if (PlayerData.cutsceneComplited)
            SceneLoader.LoadScene("StartLocation");
        background.sprite = sprites[currentSpriteIndex];
        cutsceneUI.SetActive(true);
    }

    public void NextImage()
    {
        currentSpriteIndex++;
        if (currentSpriteIndex == sprites.Count)
        {
            PlayerData.cutsceneComplited = true;
            //cutsceneUI.SetActive(false);
            SceneLoader.LoadScene("StartLocation");
        }
        else
        {
            background.sprite = sprites[currentSpriteIndex];
        }
    }
}

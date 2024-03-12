using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private CutsceneScript cutsceneController;

    [SerializeField]
    private GameObject gameModeSelector;

    [SerializeField]
    private GameObject buttonStart;

    public void OnButtonStart()
    {
        buttonStart.SetActive(false);
        gameModeSelector.SetActive(true);
    }

    public void OnButtonStartMainGame()
    {
        cutsceneController.StartCutscene();
    }

    public void OnButtonDLC()
    {
        DLC_LOLStaticData.huTaoSpawnMode = DLC_LOLStaticData.HuTaoSpwanMode.HpBarWithShaderWithoutInstanced;
        SceneLoader.LoadScene("DLC_lol");
        //SceneManager.LoadScene("DLC_lol");
    }

    public void OnButtonDLC2() 
    {
        DLC_LOLStaticData.huTaoSpawnMode = DLC_LOLStaticData.HuTaoSpwanMode.HpBarWithoutShader;
        SceneLoader.LoadScene("DLC_lol");
        //SceneManager.LoadScene("DLC_lol");
    }

    public void OnButtonDLC3()
    {
        DLC_LOLStaticData.huTaoSpawnMode = DLC_LOLStaticData.HuTaoSpwanMode.HpBarWithShaderInstanced;
        SceneLoader.LoadScene("DLC_lol");
        //SceneManager.LoadScene("DLC_lol");
    }

    public void OnButtonExit()
    {
        Application.Quit();
    }
}

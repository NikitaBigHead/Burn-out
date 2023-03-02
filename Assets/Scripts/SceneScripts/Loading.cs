using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;

    private static string sceneToLoadName = "MainMenu";
    private AsyncOperation sceneToLoad;

    public static void LoadScene(string sceneName)
    {
        sceneToLoadName = sceneName;
        SceneManager.LoadScene("Loading");
    }

    private void Start()
    {
        sceneToLoad = SceneManager.LoadSceneAsync(sceneToLoadName);
        StartCoroutine(Load());
    }
    
    IEnumerator Load()
    {
        while (!sceneToLoad.isDone)
        {
            progressBar.fillAmount = sceneToLoad.progress;
            yield return null;
        }
    }
}

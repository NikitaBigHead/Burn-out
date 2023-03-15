using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;

    private static string sceneToLoadName = "MainMenu";
    private AsyncOperation sceneToLoad;

    public static void LoadScene(string sceneName)
    {
        sceneToLoadName = sceneName;
        SceneManager.LoadScene("Loading");
        PlayerData.nextScenePosition = Vector3.zero;
    }

    public static void LoadScene(string sceneName, Vector3 location)
    {
        sceneToLoadName = sceneName;
        SceneManager.LoadScene("Loading");
        PlayerData.nextScenePosition = location;
    }

    public static void LoadScene(string sceneName, Position position)
    {
        sceneToLoadName = sceneName;
        SceneManager.LoadScene("Loading");
        PlayerData.nextScenePosition = GetLocation(position);
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

    public enum Position
    {
        Custom,
        Center,
        StartLocationHouseRightSide,
        StartLocationNearExit,
        VillageNearHouseEntrance,
        VillageNearBarnEntrance,
        VillageNearPillarEntrance,
        VillageNearCastleEntrance,
        VillageNearCoralEntrance,
        BarnAfterBosss
    }

    public static Vector3 Center = new Vector3(0f, 0f, 0f);
    public static Vector3 StartLocationHouseRightSide = new Vector3(4f, 0f, -1f);
    public static Vector3 StartLocationNearExit = new Vector3(-6.45f, 2.46f, 0f);
    public static Vector3 VillageNearHouseEntrance = new Vector3(-6.51f, 2.22f, 0f);
    public static Vector3 VillageNearBarnEntrance = new Vector3(-6.51f, 2.22f, 0f);
    public static Vector3 VillageNearPillarEntrance = new Vector3(-6.51f, 2.22f, 0f);
    public static Vector3 VillageNearCastleEntrance = new Vector3(-6.51f, 2.22f, 0f);
    public static Vector3 VillageNearCoralEntrance = new Vector3(-6.51f, 2.22f, 0f);
    public static Vector3 BarnAfterBoss = new Vector3(0f, 0f, 0f);

    public static Vector3 GetLocation(Position position)
    {
        switch (position)
        {
            case Position.Center:
                return Center;
            case Position.StartLocationHouseRightSide:
                return StartLocationHouseRightSide;
            case Position.StartLocationNearExit:
                return StartLocationNearExit;
            case Position.VillageNearHouseEntrance:
                return VillageNearHouseEntrance;
            case Position.VillageNearBarnEntrance:
                return VillageNearBarnEntrance;
            case Position.VillageNearPillarEntrance:
                return VillageNearPillarEntrance;
            case Position.VillageNearCastleEntrance:
                return VillageNearCastleEntrance;
            case Position.VillageNearCoralEntrance:
                return VillageNearCoralEntrance;
            case Position.BarnAfterBosss:
                return BarnAfterBoss;
            default: return new Vector3(0, 0, 0);
        }
    }
}
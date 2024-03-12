using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLStartupManager : MonoBehaviour
{
    [Header("Scene Bounds")]
    [SerializeField]
    private Transform bottom;
    [SerializeField]
    private Transform top;

    [SerializeField]
    private GameObject damageNumbers;

    private void Awake()
    {
        // SCENE OBJECTS DEPENDED SETTINGS
        DLC_LOLStaticData.bottom = bottom;
        DLC_LOLStaticData.top = top;
        DLC_LOLStaticData.damageNumbersPrefab = damageNumbers;

        // SCENE STATIC SETTINGS
        DLC_LOLStaticData.huTaoSpawnMode = DLC_LOLStaticData.HuTaoSpwanMode.HpBarWithShaderInstanced;
    }
}

public static class DLC_LOLStaticData
{
    // MUST BE SETTED FOR EVERY SCENE ON SCENE LOAD
    public static Transform bottom;                 // Object at scene that represent bottom left corner of scene bounds
    public static Transform top;                    // Object at scene that represent top right corner of scene bounds

    // MUST BE SETTED ON APPLICATION START
    public static GameObject damageNumbersPrefab;   // Prefab for spawning damage numbers when player dealing damage

    public enum HuTaoSpwanMode                          
    {
        HpBarWithShaderWithoutInstanced,
        HpBarWithoutShader,
        HpBarWithShaderInstanced
    }
    public static HuTaoSpwanMode huTaoSpawnMode;    // Setting for DLC_lol scene test hutao spawner


    public static Vector2 PositionInSceneBounds()
    {
        return new Vector2(Random.Range(bottom.position.x, top.position.x), Random.Range(bottom.position.y, top.position.y));
    }
}

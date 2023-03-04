#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPrefabEditor
{
    [MenuItem("YOUR_MENU_NAME/YOUR_SUBMENU_NAME/YOUR_METHOD_NAME %&n")]
    static void CreateAPrefab()
    {
        
        //Parent
        GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/Enemies/Strawman.prefab"));
        prefab.name = "TestPrefab";

        if (Selection.activeTransform != null)
        {
            prefab.transform.SetParent(Selection.activeTransform, false);
        }
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
        prefab.transform.localScale = Vector3.one;

        //Child 1
        GameObject prefabChild1 = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/Enemies/Strawman.prefab"));
        prefabChild1.name = "TestPrefabChild";

        prefabChild1.transform.SetParent(prefab.transform, false);
        prefabChild1.transform.localPosition = Vector3.zero;
        prefabChild1.transform.localEulerAngles = Vector3.zero;
        prefabChild1.transform.localScale = Vector3.one;

        //Child2...
        //...

        Selection.activeGameObject = prefab;
    }
}
#endif
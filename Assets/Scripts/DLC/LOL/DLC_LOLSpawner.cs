using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject entityPrefab;

    [SerializeField]
    private GameObject entityPrefabV;

    [SerializeField]
    private GameObject entityPrefabFinal;

    [SerializeField]
    private TMPro.TMP_Text text;

    [SerializeField]
    private TMPro.TMP_Text textSpeed;

    [SerializeField]
    private TMPro.TMP_Text textLimit;

    public float time = 2f;

    private int spawned = 0;

    private int limit = 500;

    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (spawned > limit)
        {
            return;
        }
        currentTime += Time.deltaTime;
        if (currentTime > time)
        {
            currentTime = currentTime - time;
            switch (DLC_LOLStaticData.huTaoSpawnMode)
            {
                case DLC_LOLStaticData.HuTaoSpwanMode.HpBarWithShaderWithoutInstanced:
                    Instantiate(entityPrefab);
                    break;
                case DLC_LOLStaticData.HuTaoSpwanMode.HpBarWithoutShader:
                    Instantiate(entityPrefabV);
                    break;
                case DLC_LOLStaticData.HuTaoSpwanMode.HpBarWithShaderInstanced:
                    Instantiate(entityPrefabFinal);
                    break;
            }
            spawned += 1;
            text.text = $"Objects spawned: {spawned}";
        }
    }

    public void ButtonLimitUp()
    {
        limit += 250;
        textLimit.text = $"Limit: {limit}";
    }

    public void ButtonLimitDown()
    {
        limit -= 250;
        if (limit < 0) limit = 0;
        textLimit.text = $"Limit: {limit}";
    }

    public void ButtonUp()
    {
        time += 0.1f;
        textSpeed.text = time.ToString() + " сек";
    }

    public void ButtonDown()
    {
        float newTime = time - 0.1f;
        if (newTime < 0)
        {
            newTime = 0.1f;
        }
        time = newTime;
        textSpeed.text = time.ToString() + " сек";
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Assets/Sprites/OtherSprites/SpawnerIcon.png", true);
    }
#endif
}

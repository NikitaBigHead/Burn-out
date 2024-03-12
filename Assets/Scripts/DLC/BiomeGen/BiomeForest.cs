using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeForest : MonoBehaviour
{
    [SerializeField]
    private GameObject treePrefab;

    public float baseWidth = 50f;
    public float baseHeight = 50f;
    public int baseTreeCount = 50;

    // Start is called before the first frame update
    void Start()
    {
        Generate(this.transform.position - new Vector3(baseWidth * 0.5f, baseHeight * 0.5f, 0), baseWidth, baseHeight, baseTreeCount);
    }

    public void Generate(Vector2 corner, float width, float height, int treeCount)
    {
        for (int i = 0; i < treeCount; i++) 
        {
            Vector3 pos = new Vector2(Random.Range(0, width), Random.Range(0, height)) + corner;
            Instantiate(treePrefab, pos, Quaternion.identity);
        }
        for (int i = (int)corner.x; i <= corner.x + width; i++)
        {
            Instantiate(treePrefab, new Vector3(i, corner.y, 0), Quaternion.identity);
            Instantiate(treePrefab, new Vector3(i, corner.y + height, 0), Quaternion.identity);
        }
        for (int i = (int)corner.y; i <= corner.y + height; i++)
        {
            Instantiate(treePrefab, new Vector3(corner.x, i, 0), Quaternion.identity);
            Instantiate(treePrefab, new Vector3(corner.x + width, i, 0), Quaternion.identity);
        }
    }
}

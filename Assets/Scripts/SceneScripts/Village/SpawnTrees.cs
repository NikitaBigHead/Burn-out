using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnTrees : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[0];

    GameObject tree;

    private void Start()
    {
        tree = new GameObject("tree");
        tree.AddComponent<SpriteRenderer>();
        GenerateTrees(new Vector2(1f, 42f), new Vector2(-26f, -18), new Vector2(1.1f, 1.7f));
        GenerateTrees(new Vector2(1f, 42f), new Vector2(26f, -18), new Vector2(1.1f, 1.7f));
        GenerateTrees(new Vector2(52f, 1f), new Vector2(-26f, 25), new Vector2(1.7f, 1.1f));
        GenerateTrees(new Vector2(52f, 1f), new Vector2(-26f, -18), new Vector2(1.7f, 1.1f), 6);
    }

    /// <summary>
    /// Создаёт деревья в выбранной области
    /// </summary>
    /// <param name="size">Размер участка в котором будут генерироваться деревья</param>
    /// <param name="offset">Смещение от центра координат</param>
    /// <param name="delta">Расстояние между соседними деревьями</param>
    public void GenerateTrees(Vector2 size, Vector2 offset, Vector2 delta, int orderInLayer=3, int maxTrees=50)
    {
        int cnt = 0;
        Vector2 current = new Vector2(0, 0);
        while (++cnt < maxTrees)
        {
            SpriteRenderer treeSpriteRenderer = Instantiate(tree).GetComponent<SpriteRenderer>();
            treeSpriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            treeSpriteRenderer.sortingOrder = orderInLayer;
            if (Random.Range(0, 2) == 0) treeSpriteRenderer.flipX = true;
            treeSpriteRenderer.transform.position = new Vector3(current.x + offset.x, current.y + offset.y, (current.x + 0.1f) * (current.y + 0.1f));
            current += delta;
            if (current.x > size.x && current.y > size.y) break;
            if (current.x > size.x && current.y < size.y) current.x = 0;
            if (current.x < size.x && current.y > size.y) current.y = 0; 
        }
    }

    public IEnumerator GenerateTreesE(Vector2 size, Vector2 offset, Vector2 delta, float spawnDelay = 0.7f, int orderInLayer=3, int maxTrees = 50)
    {
        int cnt = 0;
        Vector2 current = new Vector2(0, 0);
        while (++cnt < maxTrees)
        {
            SpriteRenderer treeSpriteRenderer = Instantiate(tree).GetComponent<SpriteRenderer>();
            treeSpriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            treeSpriteRenderer.sortingOrder = 3;
            if (Random.Range(0, 2) == 0) treeSpriteRenderer.flipX = true;
            treeSpriteRenderer.transform.position = new Vector3(current.x + offset.x, current.y + offset.y, (current.x + 0.1f) * (current.y + 0.1f));
            current += delta;
            if (current.x > size.x && current.y > size.y) break;
            if (current.x > size.x && current.y < size.y) current.x = 0;
            if (current.x < size.x && current.y > size.y) current.y = 0;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D)), RequireComponent(typeof(MeshFilter))]
public class VisionCircle : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private MeshFilter meshFilter;

    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        mesh = new Mesh();
        mesh.vertices = verts; 
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
        for (int i = 0; i < maxBlockers; i++)
        {
            stack.Push(maxBlockers - i - 1);
            for (int j = 0; j < vertexCount; j++) 
            {
                uv[i * vertexCount + j] = uvBase[j];
            }
        }
        mesh.uv = uv;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        UpdatePositions();
    }

    public void UpdatePositions()
    {
        foreach (var blocker in blockers)
        {
            int id = blocker.Value;
            MyCalc(this.gameObject.transform.position, blocker.Key.transform.position, 0.5f, id);
            mesh.vertices = verts;
        }
    }

    void MyCalc(Vector2 dotPos, Vector2 cPos, float rad, int id)
    {
        float L = Vector2.Distance(cPos, dotPos);

        float cosA = rad / L;

        float sinA2 = (1 - cosA * cosA);
        float sinA = Mathf.Sqrt(sinA2);

        float xd = (cPos.x - dotPos.x) / L;
        float yd = (cPos.y - dotPos.y) / L;

        Vector2 t1n = new Vector2(cosA * xd - sinA * yd, cosA * yd + sinA * xd);
        Vector2 t2n = new Vector2(cosA * xd + sinA * yd, cosA * yd - sinA * xd);

        Vector2 t1 = cPos - t1n * rad;
        Vector2 t2 = cPos - t2n * rad;

        Vector2 t1end = t1 + new Vector2(t1n.y, -t1n.x) * circleCollider2D.radius;
        Vector2 t2end = t2 + new Vector2(-t2n.y, t2n.x) * circleCollider2D.radius;
        Vector2 end = cPos + new Vector2(xd, yd) * circleCollider2D.radius;

        int ind = id * vertexCount;
        verts[ind + 0] = t2;
        verts[ind + 0] -= transform.position;
        verts[ind + 1] = t2end;
        verts[ind + 1] -= transform.position;
        verts[ind + 2] = end;
        verts[ind + 2] -= transform.position;
        verts[ind + 3] = t1end;
        verts[ind + 3] -= transform.position;
        verts[ind + 4] = t1;
        verts[ind + 4] -= transform.position;
    }

    private Vector2[] uvBase = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0.5f, 1), new Vector2(1, 1), new Vector2(1, 0) };

    private int[] trianglesBase = new int[] { 0, 1, 2, 0, 2, 4, 4, 2, 3 };
    private int[] triangles = new int[] { };

    public static int indexCount = 9;
    public static int vertexCount = 5;
    public static int maxBlockers = 255;

    private Vector3[] verts = new Vector3[vertexCount * maxBlockers];
    private Vector2[] uv = new Vector2[vertexCount * maxBlockers];

    Stack<int> stack = new Stack<int>(maxBlockers);
    Dictionary<GameObject, int> blockers = new Dictionary<GameObject, int>();
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("VisionBlocker"))
        {
            int id = stack.Pop();
            MyCalc(this.gameObject.transform.position, collision.gameObject.transform.position, 0.5f, id);
            blockers.Add(collision.gameObject, id);
            Array.Resize(ref triangles, triangles.Length + indexCount);
            for (int i = indexCount; i > 0; i--)
            {
                triangles[triangles.Length - i] = trianglesBase[indexCount - i] + id * vertexCount;
            }
            mesh.triangles = triangles;
            mesh.vertices = verts;
/*#if DEBUG
            Debug.Log($"Enter {collision.name} id:{id}");
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
#endif*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("VisionBlocker"))
        {
            int id = blockers[collision.gameObject];
            int tId = 0;
            for (int i = 0; i < triangles.Length; i += indexCount)
            {
                if (triangles[i] == id * vertexCount)
                {
                    tId = i;
                    break;
                }
            }
            for (int i = tId; i < triangles.Length - indexCount; ++i)
            {
                triangles[i] = triangles[i + indexCount];
            }
            Array.Resize(ref triangles, triangles.Length - indexCount);
            mesh.triangles = triangles;
            blockers.Remove(collision.gameObject);
            stack.Push(id);
/*#if DEBUG
            Debug.Log($"Exit {collision.name} id:{id}");
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
#endif*/
        }
    }
}
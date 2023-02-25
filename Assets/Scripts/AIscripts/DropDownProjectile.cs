using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownProjectile : MonoBehaviour
{
    public float speed = 0.0f;
    public float size = 1.0f;
    public float height = 1.0f;
    public Vector2 target = Vector2.zero;

    [SerializeField]
    private GameObject shadow;
    [SerializeField]
    private GameObject main;

    public void Launch(float speed, float size, float height, Vector2 target)
    {
        this.speed = speed;
        this.size = size;
        this.height = height;
        transform.localScale = new Vector3(size, size, 1.0f);
        this.target = target;
        shadow.transform.position = new Vector3(target.x, target.y, 3);
        main.transform.position = new Vector3(target.x, target.y + height, -1);
    }

    protected void FixedUpdate()
    {
        Vector3 translation = new Vector3(0, -speed, 0);
        main.transform.position += translation;
        height -= speed;
        if (height <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

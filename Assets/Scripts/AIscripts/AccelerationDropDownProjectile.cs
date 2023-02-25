using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationDropDownProjectile : MonoBehaviour
{
    public float speed;
    public float size = 1.0f;

    public Vector3 eq;
    public Vector3 shadow_eq;

    public Vector2 target;

    [SerializeField]
    private GameObject shadow;
    [SerializeField]
    private GameObject main;

#if DEBUG
    private GameObject mark;
#endif

    public void Launch(Vector2 spawnPos, Vector2 peakPos, Vector2 targetPos, float height, float speed, float size)
    {
        this.size = size;
        this.speed = speed;
        eq = Parabola.FindParabola(new Vector2(spawnPos.x, spawnPos.y + height), peakPos, targetPos);
        main.transform.position = new Vector3(spawnPos.x, spawnPos.y + height, 0);
        shadow.transform.position = new Vector3(spawnPos.x, spawnPos.y, 0);
        shadow_eq = new Vector3((targetPos.y - spawnPos.y) / (targetPos.x - spawnPos.x), spawnPos.x, spawnPos.y);
        target = targetPos;
#if DEBUG
        mark = Instantiate(shadow, this.transform);
        mark.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        mark.transform.position = new Vector3(targetPos.x , targetPos.y, 1);
#endif
    }

    protected void FixedUpdate()
    {
        float main_x = main.transform.position.x + speed;
        float main_y = main_x * main_x * eq.x + eq.y * main_x + eq.z;
        main.transform.position = new Vector3(main_x, main_y, 0);

        float shadow_y = (main_x - shadow_eq.y) * shadow_eq.x + shadow_eq.z;
        shadow.transform.position = new Vector3(main_x, shadow_y, 0);

        if (main_x > target.x)
        {
            Destroy(this.gameObject);
        }
    }
}

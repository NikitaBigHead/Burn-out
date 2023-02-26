using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnWall : MonoBehaviour
{
    [SerializeField]
    private List<Color> colors;

    [SerializeField]
    private SpriteRenderer sprite;

    private int current = 0;

    private void Start()
    {
        current = 0;
        if (colors.Count == 0) colors = new List<Color>() { Color.white, new Color(1f, 0.75f, 0.75f), new Color(0.8f, 0.5f, 0.5f), new Color(0.65f, 0.25f, 0.25f), new Color(0.5f, 0f, 0f) };
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = colors[current];
    }

    public void ReceiveDamage(float value)
    {
        current++;
        if (current >= colors.Count) OnDeath();
        else
        {
            sprite.color = colors[current];
        }
    }

    public void OnDeath()
    {
        sprite.color = Color.black;
    }
}

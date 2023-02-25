using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite_renderer;
    Collider2D coll;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        coll = GetComponentInChildren<CapsuleCollider2D>();
        this.sprite_renderer.enabled = false;
        coll.enabled = false;
    }

    public float attackDelayTime = 0.5f;
    bool perform_attack = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !perform_attack)
        {
            Vector2 v2 = new Vector2(Input.mousePosition.x / Screen.width - 0.5f, Input.mousePosition.y / Screen.height - 0.5f).normalized;
            this.transform.localPosition = v2 * 1.3f;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (float)(180 / Math.PI * Math.Atan2(v2.y, v2.x) - 45f)));
            this.sprite_renderer.enabled = true;
            coll.enabled = true;
            anim.Play("attack_animation");
            perform_attack = true;
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelayTime);
        anim.Play("New State");
        perform_attack = false;
        this.sprite_renderer.enabled = false;
        coll.enabled = false;
    }
}

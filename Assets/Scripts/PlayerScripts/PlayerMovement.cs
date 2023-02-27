using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;

    public Vector2 direction;
    private Rigidbody2D rb;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Debug.Log(anim);
        if (PlayerData.prefab == null) PlayerData.prefab = this.gameObject;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if(direction == Vector2.zero)
        {
            anim.Play("PlayerIdleForward");

        }
        if (rb.position.y - direction.y < 0)
        {
            anim.Play("PlayerWalkBack");
        }
        else if(rb.position.y - direction.y > 0)
        {
            anim.Play("PlayerWalk");
            //anim.SetBool("IsStay", false);
            //anim.SetBool("IsGoBack", false);
            //anim.SetBool("IsGoForward", true);
        }
        rb.MovePosition(rb.position + direction.normalized*speed);
          
    }
}

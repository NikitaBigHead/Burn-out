using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;

    public Vector2 direction;
    private Rigidbody2D rb;

    private Animator anim;

    private bool isForward = true;
    private void Awake()
    {
        anim = GetComponent<Animator>();
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
        Vector2 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position ;

        if(direction == Vector2.zero)
        {
            if (mouseDir.y<0) anim.Play("PlayerIdleForward");
            else anim.Play("PlayerIdleBack");
        }

        else if(direction.x==0 || direction.y == 0)
        {
            if (direction.y != 0)
            {
                if (mouseDir.y > 0)
                {
                    anim.Play("PlayerWalkBack");
                    isForward = false;
                }
                else if (mouseDir.y < 0)
                {
                    anim.Play("PlayerWalk");
                    isForward = true;
                }
            } 
            if (direction.x != 0)
            {
                if (mouseDir.x < 0)
                {
                    anim.Play("PlayerLeft");
                }
                else if (mouseDir.x > 0)
                {
                    anim.Play("PlayerRight");
                }
            }
           
        }
        else if (direction.x != 0 && direction.y != 0)
        {
            if (mouseDir.y > 0)
            {
                anim.Play("PlayerWalkBack");
                isForward = false;
            }
            else if (mouseDir.y < 0)
            {
                anim.Play("PlayerWalk");
                isForward = true;
            }
        }
      
        /*
        if(direction.x!= 0) {
            if (isForward) anim.Play("PlayerWalk");
            else anim.Play("PlayerWalkBack");
        }
        */
        rb.MovePosition(rb.position + direction.normalized*speed);
          
    }
}

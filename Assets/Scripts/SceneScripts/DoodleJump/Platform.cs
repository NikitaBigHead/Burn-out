using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    bool isJumped = false;
    private GameController gameController;
    private void Awake()
    {
        gameController = GameObject.FindWithTag("Game").gameObject.GetComponent<GameController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Body")
        {
            if (!isJumped) gameController.AddScore();
            isJumped = true;
        }
        else if (collision.collider.name == "DeadZone")
        {
            Destroy(this.gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public delegate void CollisionAction(GameObject sender, GameObject collision);
    public CollisionAction OnEnterCollision;
    public CollisionAction OnExitCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnterCollision(this.gameObject, collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExitCollision(this.gameObject, collision.gameObject);
    }
}

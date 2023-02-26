using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AttackSimpStawman : MonoBehaviour
{
    private Transform target;
    public float attackDistance = 1f;

    private bool canAttack = true;
    public Hand hand;

    private List<SpriteRenderer> spriteRenderers;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderers = gameObject.GetComponent<SpriterManager>().renderers;
    }
    void Start()
    {
        
    }


    void FixedUpdate()
    {

        Vector2 diff = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        if (diff.x < 0)
        {
            for(int i = 0; i < spriteRenderers.Count; i++)
            {
                spriteRenderers[i].flipX = true;
            }
        }
        else
        {
            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                spriteRenderers[i].flipX = false;
            }
        }
        
        if (diff.magnitude < attackDistance && canAttack)
        {
            canAttack = false;
            Invoke("ResetAttack", hand.PerformAttack(diff));
        }
    }
    private void ResetAttack()
    {
        canAttack = true;
    }
}

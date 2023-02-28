using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class JumperAi : MonoBehaviour
{
    [SerializeField]
    private List<Transform> targets;

    [SerializeField]
    private Transform player;

    private Transform currentTarget;

    private NavMeshAgent agent;

    public float jumpDelay = 0.35f;
    public float jumpLength = 0.35f;

    public int targetsBeforePlayer = 2;

    private int targetsVisited = 0;
    private bool canJump = true;
    private bool targetExist = false;



    public float projectileSize = 1f;
    public float projectileSpeed = 1f;
    public float projectileRange = 10f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (targets.Count == 0)
        {
            foreach (var target in FindObjectsOfType<JumpersTarget>())
            {
                targets.Add(target.gameObject.GetComponent<Transform>());
            }
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    private void Update()
    {

#if DEBUG
        if (currentTarget != null)
            Debug.DrawLine(transform.position, currentTarget.position, Color.red);
#endif

        if (canJump)
        {
            // Если цель не выбрана
            if (!targetExist)
            {
                // Если количество посещённых целей меньше чем заднанное, то выбераем любую цель кроме игрока, иначе идём к игроку
                if (targetsVisited < targetsBeforePlayer)
                {
                    currentTarget = targets[Random.Range(0, targets.Count)];
                }
                else
                {
                    currentTarget = player;
                }
                targetExist = true;
            }

            canJump = false;
            Vector3 nextPos = new Vector2(currentTarget.position.x - transform.position.x, currentTarget.position.y - transform.position.y).normalized;
            nextPos = transform.position + nextPos * jumpLength;

            if (new Vector2(nextPos.x - currentTarget.position.x, nextPos.y - currentTarget.position.y).magnitude < jumpLength)
            {
                targetExist = false;
                targetsVisited += 1;
            }

            agent.SetDestination(nextPos);
            Invoke("ResetJump", jumpDelay);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {

            Destroy(this.gameObject.GetComponent<JumperAi>());
            SimpleProjectile projectile = this.gameObject.AddComponent<SimpleProjectile>();

            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            projectile.Launch(projectileSpeed, projectileSize, projectileRange, direction);
            projectile.isPLayerRecaptured = true;
        }
    }

}

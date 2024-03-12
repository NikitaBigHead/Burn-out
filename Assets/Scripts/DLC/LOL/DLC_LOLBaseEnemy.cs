using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DLC_LOLBaseEnemy : MonoBehaviour
{
    [SerializeField]
    private DLC_LOLEntityController controller;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField] 
    private SpriteRenderer hpBar;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        controller.EventOnReceiveDamage += OnHealthChange;
        controller.EventOnReceiveHeal += OnHealthChange;
        agent.SetDestination(transform.position + new Vector3(0, 1, 0));
    }

    private void OnHealthChange(float value)
    {
        MaterialPropertyBlock matBlock = new MaterialPropertyBlock();
        hpBar.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Health", controller.stats.health / controller.stats.maxHealth);
        hpBar.SetPropertyBlock(matBlock);

        DLC_LOLFadingText text = Instantiate(DLC_LOLStaticData.damageNumbersPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(0, 2f), 0), Quaternion.identity).GetComponent<DLC_LOLFadingText>();
        text.text.text = value.ToString("F0");
        text.gameObject.SetActive(true);
    }

    public float timeToRethink = 5f;

    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToRethink)
        {
            agent.SetDestination(DLC_LOLStaticData.PositionInSceneBounds());
            time = 0;
        }
    }
}

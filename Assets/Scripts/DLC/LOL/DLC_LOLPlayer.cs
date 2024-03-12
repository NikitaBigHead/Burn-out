using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DLC_LOLPlayer : MonoBehaviour
{
    [SerializeField]
    private DLC_LOLHeroController heroController;

    [SerializeField]
    private NavMeshAgent agent;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void OnKeySpace()
    {
        DLC_LOLHudController.hudController.Select(heroController);
    }

    public void OnKeyQ()
    {
        heroController.UseSkill1();
    }

    public void OnClick()
    {
        agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}

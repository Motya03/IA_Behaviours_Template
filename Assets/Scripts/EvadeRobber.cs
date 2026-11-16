using UnityEngine;
using UnityEngine.AI;

public class EvadeRobber : StateRobber
{
    private Vector3 velocity;
    private Drive targetDrive;

    private float predictionTime = 1f;

    public EvadeRobber(
        NavMeshAgent _agent,
        Transform _target,
        GameObject _npc
    ) : base(_agent, _target, _npc)
    {
        name = STATE.EVADE;

        targetDrive = _target.GetComponent<Drive>();
    }

    public override void Enter()
    {
        Debug.Log("Evade");
        base.Enter();
    }

    public override void Update()
    {
        Vector3 futurePos = target.position + velocity * predictionTime;

        Vector3 evadeDirection = npc.transform.position - futurePos;

        evadeDirection.Normalize();

        agent.SetDestination(evadeDirection);
    }

   
}
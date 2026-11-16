using UnityEngine;
using UnityEngine.AI;

public class PursueRobber : StateRobber
{
    private Vector3 velocity;
    private Drive targetDrive;

    private float predictionTime = 1f;

    public PursueRobber(
        NavMeshAgent _agent,
        Transform _target,
        GameObject _npc
    ) : base(_agent, _target, _npc)
    {
        name = STATE.PURSUE;

        targetDrive = _target.GetComponent<Drive>();
    }

    public override void Enter()
    {
        Debug.Log("Pursue");
        base.Enter();
    }

    public override void Update()
    {
        CheckVelocity();

        Vector3 futurePos =
            -target.position + velocity * predictionTime;

        agent.SetDestination(futurePos);
    }

    private void CheckVelocity()
    {
        velocity = targetDrive.velocity;
    }
}
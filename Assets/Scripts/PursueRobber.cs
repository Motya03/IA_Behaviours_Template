using UnityEngine;
using UnityEngine.AI;

public class PursueRobber : StateRobber
{
    private Vector3 velocity;
    private Drive targetDrive;

     private float minPredictionTime = 0.2f;
     private float maxPredictionTime = 1f;

     private float minDistance = 2f;
     private float maxDistance = 10f;
 
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
        if (complexOn)
        {
            bool canSeeMee = CanTargetSeeMe();
            bool canISee = CanSeeTarget();
            bool isClose = IsTargetInRange();
            if (canSeeMee)
            {
                nextState = new EvadeRobber(agent, target, npc);
                stage = EVENT.EXIT;
            }

        }
        CheckVelocity();
        CalculatePredictionTime();

        Vector3 futurePos = target.position + velocity * predictionTime;

        agent.SetDestination(futurePos);
    }

    private void CheckVelocity()
    {
        velocity = targetDrive.velocity;
    }

    private void CalculatePredictionTime()
    {
        float distance = Vector3.Distance(
            target.position,
            npc.transform.position
        );

        float t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        predictionTime = Mathf.Lerp(
            minPredictionTime,
            maxPredictionTime,
            t
        );
    }
}
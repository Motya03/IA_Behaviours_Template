using UnityEngine;
using UnityEngine.AI;

public class EvadeRobber : StateRobber
{
    private Vector3 velocity;
    private Drive targetDrive;



    private float minPredictionTime = 0.2f;
    private float maxPredictionTime = 1f;

    private float minDistance = 2f;
    private float maxDistance = 10f;

    private float predictionTime = 1f;
    private float evadeDistance = 10f;

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
        if (complexOn)
        {
            bool isClose = IsTargetInRange();
            if (!isClose)
            {
                nextState = new HideRobber(agent, target, npc);
                stage = EVENT.EXIT;
            }
        }
            CalculatePredictionTime();
        // Get player velocity
        velocity = targetDrive.velocity;

        // Predict future player position
        Vector3 futurePos = target.position + velocity * predictionTime;

        // Calculate opposite direction
        Vector3 evadeDirection = npc.transform.position - futurePos;

        evadeDirection.Normalize();

        // Calculate destination
        Vector3 evadeDestination =
            npc.transform.position + evadeDirection * evadeDistance;

        // Move enemy
        agent.SetDestination(evadeDestination);
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
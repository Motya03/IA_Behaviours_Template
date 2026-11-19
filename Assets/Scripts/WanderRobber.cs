using UnityEngine;
using UnityEngine.AI;

public class WanderRobber : StateRobber
{
    private Vector3 wanderTarget;

    private float radius = 3f;
    private float distance = 5f;
    private float jitter = 0.2f;

    private float updateRate = 0.2f;
    private float timer;

    public WanderRobber(
        NavMeshAgent _agent,
        Transform _target,
        GameObject _npc
    ) : base(_agent, _target, _npc)
    {
        name = STATE.WANDER;
    }

    public override void Enter()
    {
        Debug.Log("Wander");
        base.Enter();

        // Initial wander target
        wanderTarget = Vector3.forward * radius;
        timer = 0f;
    }

    public override void Update()
    {
      
        timer += Time.deltaTime;

        if (timer >= updateRate)
        {
            timer = 0f;

            CalculateWander();
        }
        
        if (complexOn)
        {
           // Debug.Log("complex");
            bool canSeeMee = CanTargetSeeMe();
            bool canISee = CanSeeTarget();
            bool isClose = IsTargetInRange();
          
            if (isClose)
            {
                if (!canSeeMee && canISee)
                {
                    nextState = new PursueRobber(agent, target, npc);
                    stage = EVENT.EXIT;
                }
                else

                {
                    nextState = new EvadeRobber(agent, target, npc);
                    stage = EVENT.EXIT;
                }
            }
        }
    }

    private void CalculateWander()
    {
        // Random variation
        Vector3 randomVector = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        );

        wanderTarget += randomVector * jitter;

        // Keep target on circle
        wanderTarget = wanderTarget.normalized * radius;

        // Calculate target position
        Vector3 targetPos =
            npc.transform.position +
            npc.transform.forward * distance +
            npc.transform.right * wanderTarget.x +
            npc.transform.forward * wanderTarget.z;

        // Move agent
        agent.SetDestination(targetPos);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
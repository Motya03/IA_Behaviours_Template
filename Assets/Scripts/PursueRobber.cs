using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PursueRobber : StateRobber
{

    private Vector3 velocity;
    private int t = 3;
    public PursueRobber(NavMeshAgent _agent, Transform _target, GameObject _npc): base(_agent, _target, _npc)
    {
        name = STATE.PURSUE;
       
    }






    public override void Enter()
    {
      // velocity = new Vector3(0,0,2);
        Debug.Log("Pursue");
        base.Enter();
    }

    public override void Update()
    {
        Debug.Log(velocity);
        CheckVelocity();
        Vector3 futurePos = target.position + velocity * t;
        agent.SetDestination(futurePos);

        //  stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void CheckVelocity()
    {
        velocity = _velocity;
    }




}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class IdleRobber : StateRobber
{
   // StateRobber currentState;

    public IdleRobber(NavMeshAgent _agent, Transform _target, GameObject _npc) : base(_agent, _target, _npc)
    {
        name = STATE.IDLE;
     
       
    }



    public override void Enter()
    {

        Debug.Log("333333");
        base.Enter();
    }

    public override void Update()
    {
        
     //  stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }

    


}

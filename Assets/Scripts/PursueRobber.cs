using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PursueRobber : StateRobber
{
   
    

    public PursueRobber(NavMeshAgent _agent, GameObject _target, GameObject _npc): base(_agent, _target, _npc)
    {
        name = STATE.PURSUE;
       
    }






    public override void Enter()
    {

        Debug.Log("Pursue");
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

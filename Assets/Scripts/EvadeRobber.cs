using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EvadeRobber : StateRobber
{
   
    
    private int t = 3;
    private Vector3 futurePos;
    private float velocity = 3f;
    public EvadeRobber(NavMeshAgent _agent, GameObject _target, GameObject _npc): base(_agent, _target, _npc)
    {
        name = STATE.EVADE;
       
    }






    public override void Enter()
    {
        
        Debug.Log("Evade");
        base.Enter();
    }

    public override void Update()
    {
        //futurePos = target.position + target.velocity * t;
        //  stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }

    


}

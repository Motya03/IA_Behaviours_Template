using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class WanderRobber : StateRobber
{

    private Vector3 velocity;
    private int t = 3;
    public WanderRobber(NavMeshAgent _agent, Transform _target, GameObject _npc): base(_agent, _target, _npc)
    {
        name = STATE.WANDER;
       
    }






    public override void Enter()
    {
     
        Debug.Log("Wander");
        base.Enter();
    }

    public override void Update()
    {

        wanderTarget += randomVector * jitter;
        wanderTarget = Normalize(wanderTarget) * radius;
        Vector3 targetPos = transform.position + transform.forward * distance + wanderTarget;
        agent.SetDestination(targetPos);

    }

    public override void Exit()
    {
        base.Exit();
    }

   




}

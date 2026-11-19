using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class StateRobber
{
    public enum STATE
    {
        HIDE, PURSUE, EVADE, WANDER, COMPLEX , IDLE
    };
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };
    public STATE name;
    protected EVENT stage;
    protected StateRobber nextState;
    protected NavMeshAgent agent;
    protected Transform target;
    protected GameObject npc;
    protected static bool complexOn;
   




    public StateRobber( NavMeshAgent _agent, Transform _target, GameObject _npc)
    {
        target = _target;
        agent = _agent;
        npc = _npc;
        
        stage = EVENT.ENTER;

    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }


    public StateRobber Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            Debug.Log("Siguiente estado: " + nextState.name);
            return nextState;

        }
        return this;
    }

    public void ChangeState(StateRobber newState)
    {
       

        // Prevent changing to the same state type
        if (GetType() == newState.GetType())
            return;

        Debug.Log("Cambia");

        stage = EVENT.EXIT;
        nextState = newState;
    }


    public void ComplexBehaviour(bool complex)
    {
        complexOn = complex;
    }



    public bool CanSeeTarget()
    {
        Vector3 forward = npc.transform.forward;
        Vector3 origin = npc.transform.position;

        for (float angle = 0; angle < 60; angle += 10f)
        {
            Vector3 direction =
                Quaternion.Euler(0, angle, 0) * forward;

            Debug.DrawRay(origin, direction * 10f, Color.red);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, 10f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    
                    Debug.DrawLine(origin, hit.point, Color.green);
                    
                   // Debug.Log("CanSeeTarget");
                    return true;
   
                }
            }
        }
        return false;
    }
    public bool CanTargetSeeMe()
    {
        Vector3 origin = target.position;
        Vector3 forward = target.forward;
        for (float angle = 0; angle < 60; angle += 10f)
        {
            Vector3 direction =
                Quaternion.Euler(0, angle, 0) * forward;
            Debug.DrawRay(origin, direction * 10f, Color.red);


            if (Physics.Raycast(origin, direction, out RaycastHit hit, 10f))
            {
                if (hit.collider.CompareTag("Robber"))
                {
                  
                    Debug.DrawLine(origin, hit.point, Color.green);
                   // Debug.DrawRay(point, Vector3.up, Color.blue);
                    return true;

                }
            }
        }
        return false;
    }
    public bool IsTargetInRange()
    {

        float distance = Vector3.Distance(
           target.position,
           npc.transform.position
       );
        if (distance < 10)
        {
           // Debug.Log("InRange");
            return true;
        }
        return false;
    }




}
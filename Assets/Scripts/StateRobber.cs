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

    
    


}
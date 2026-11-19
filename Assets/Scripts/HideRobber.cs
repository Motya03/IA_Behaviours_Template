using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HideRobber : StateRobber
{
    private List<Vector3> hidePoints = new List<Vector3>();
    private Vector3 startPosition;
    private bool hasMoved;


    public HideRobber(NavMeshAgent _agent, Transform _target, GameObject _npc): base(_agent, _target, _npc)
    {
        name = STATE.HIDE;
       agent = _agent;
        startPosition = agent.transform.position;
    }






    public override void Enter()
    {
        
        Debug.Log("22222");
        FindHidePoints();
        
        
        base.Enter();
    }

    public override void Update()
    {
        if (complexOn)
        {
         
            float distanceFromStart = Vector3.Distance(
                agent.transform.position,
                startPosition
            );

            // Primero esperamos a que se aleje del origen.
            if (!hasMoved && distanceFromStart > 2f)
            {
                hasMoved = true;
            }

            // Una vez se ha movido, comprobamos si ha llegado.
            if (hasMoved &&
                !agent.pathPending &&
                agent.remainingDistance <= agent.stoppingDistance)
            {
               
                if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f  )
                {
                    bool canSeeMee = CanTargetSeeMe();
                    if (canSeeMee)
                    {
                        Debug.Log("To idle");
                        nextState = new IdleRobber(agent, target, npc);
                        stage = EVENT.EXIT;
                    }
                    
                }
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    private void FindHidePoints()
    {
        hidePoints.Clear();

        Vector3 origin = target.position;

        for (float angle = 0; angle < 360; angle += 20f)
        {
            Vector3 direction =
                Quaternion.Euler(0, angle, 0) * Vector3.forward;

            Debug.DrawRay(origin, direction * 10f, Color.red);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, 10f))
            {
                if (hit.collider.CompareTag("hide"))
                {
                    Vector3 point = hit.point + direction * 4f;

                    hidePoints.Add(point);

                    Debug.DrawLine(origin, hit.point, Color.green);
                    Debug.DrawRay(point, Vector3.up, Color.blue);
                }
            }
        }

        GetClosestHidePoint(hidePoints);
    }


    private Vector3 GetClosestHidePoint(List<Vector3> points)
    {
        Vector3 closestPoint = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(npc.transform.position, point);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }
        agent.SetDestination(closestPoint);
        

        return closestPoint;
    }


}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class IncompletoBot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Drive playerCod;
    private bool hideDone;

    public bool complexOn;



    StateRobber currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = new IdleRobber(agent, player, this.gameObject);
        agent = this.GetComponent<NavMeshAgent>();

    }
    [SerializeField] private float rayDistance = 50f;
    [SerializeField] private float angleStep = 20f;
    
    private void Update()
    {
        
         currentState = currentState.Process();
      


    }
   

    
    public void HideInput(InputAction.CallbackContext context)
    {
        if (hideDone) return;
        Debug.Log("Hola");
        switch (context.phase)
        {
            case InputActionPhase.Started:

               
                hideDone = true;
                Hide();
               
                break;
        }
    }

    public void PursueInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:

                Pursue();
                break;
        }
    }

    public void EvadeInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:

                Evade();
                break;
        }
    }
    public void WanderInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:

                Wander();
                break;
        }
    }

    public void ComplexInput(InputAction.CallbackContext context)
    {
        
        switch (context.phase)
        {
            case InputActionPhase.Started:

                if (complexOn)
                {
                    complexOn = false;
                    CleverHide(false);
                    break;
                }
                else
                {
                    complexOn = true;
                    CleverHide(true);
                    break;
                }
               // break;
              
        }
    }





    //void Seek(Vector3 location)
    //{
    //    //TODO
    //}
    ////En seek ponemos como destino la posicion del target, en Flee (huir) ponemos como
    ////destino la posicion del target en negativo para que vaya huyendo de esa posicion
    //void Flee(Vector3 location)
    //{
    //   //TODO
    //}

    void Pursue()
    {
        currentState.ChangeState(new PursueRobber(agent, player, this.gameObject));
    }

    //Evade es practicamente igual a Pursue solo que se debe ir en la direccion contraria
    void Evade()
    {
        currentState.ChangeState(new EvadeRobber(agent, player, this.gameObject));
    }


	//Para Wander necesitamos una var. que recordemos en cada llamada y por lo
	//tanto no puede ser local al metodo.
    //Vector3 wanderTarget = Vector3.zero; //Se actualiza cada vez que el agente
										 //tiene un nuevo valor en su posicion
    void Wander()
    {
        currentState.ChangeState(new WanderRobber(agent, player, this.gameObject));
    }

    void Hide()
    {
        
        //FindHidePoints();
        currentState.ChangeState(new HideRobber(agent, player, this.gameObject));
        Debug.Log("Hola");
        hideDone = false;
        //StartCoroutine(CoverBehaviour());
        // currentState = new HideRobber(agent, player);
    }

    void CleverHide(bool on)
    {
        currentState.ChangeState(new IdleRobber(agent, player, this.gameObject));

        currentState.ComplexBehaviour(on);
    }
   


    bool CanSeeTarget()
    {
        //TODO
        return false;
    }

    //En el siguiente comportamiento el robber se acerca al cop mientras este no le mira pero, cuando esto sucede, el robber se esconde.
    //Tenemos que decidir cuando el cop ve al rubber, digamos que esto sucede si esta en su angulo de vision (p.e. 60�)
    bool TargetCanSeeMe()
    {
        //TODO
        return false;
    }


    // Update is called once per frame
   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class IncompletoBot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;
    private bool hideDone; 




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



    void Seek(Vector3 location)
    {
        //TODO
    }
    //En seek ponemos como destino la posicion del target, en Flee (huir) ponemos como
    //destino la posicion del target en negativo para que vaya huyendo de esa posicion
    void Flee(Vector3 location)
    {
       //TODO
    }

    void Pursue()
    {
        currentState.ChangeState(new PursueRobber(agent, player, this.gameObject));
    }

    //Evade es practicamente igual a Pursue solo que se debe ir en la direccion contraria
    void Evade()
    {
        //TODO
    }


	//Para Wander necesitamos una var. que recordemos en cada llamada y por lo
	//tanto no puede ser local al metodo.
    Vector3 wanderTarget = Vector3.zero; //Se actualiza cada vez que el agente
										 //tiene un nuevo valor en su posicion
    void Wander()
    {
        //TODO
    }

    void Hide()
    {
        
        //FindHidePoints();
        currentState.ChangeState(new HideRobber(agent, player, this.gameObject));
        Debug.Log("Hola");
        //StartCoroutine(CoverBehaviour());
       // currentState = new HideRobber(agent, player);
    }

    void CleverHide()
    {
        //TODO
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
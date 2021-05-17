using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector]

    public float stoppingDistance = 0.5f;
    public bool isActive = false;
    public bool isInteracting = false;

    public NavMeshAgent playerAgent;
    public GameObject Player;
    public virtual void MoveToInteract(NavMeshAgent playerAgent)
    {
        //playerAgent = player.GetComponent<NavMeshAgent>();
        //Player = player;
        playerAgent.destination = transform.position;
        playerAgent.stoppingDistance = stoppingDistance;
        //isActive = true;
    }

    void Update()
    {
        if (playerAgent && !isInteracting && !playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= stoppingDistance)
            { 
                Interact();
                isActive = false;
            }
        } 
    }

    public virtual void Interact()
    {
    }

    public virtual void AfterInteract()
    {
    }
}

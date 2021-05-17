using UnityEngine;
using System.Collections;

public class WorldInteraction : Interactable
{
    UnityEngine.AI.NavMeshAgent playerAgent;
    PlayerWeaponController WeaponController;
    [SerializeField]
    private LayerMask interactLayer;

    void Start()
    {
        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        WeaponController = GetComponent<PlayerWeaponController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }
        else if (Input.GetMouseButtonDown(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            WeaponController.PerformWeaponAttack();
        }

        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
    }

    void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.SphereCast(interactionRay, 1f, out interactionInfo, Mathf.Infinity, interactLayer))
        {
            playerAgent.updateRotation = true;
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Enemy")
            {
                Debug.Log("move to enemy");
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
                if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
                {
                    WeaponController.PerformWeaponAttack();
                }
            }
            else if (interactedObject.tag == "Interactable Object")
            {
                var interaction = interactedObject.GetComponent<Interactable>();
                if (interaction)
                    interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else
            {
                playerAgent.stoppingDistance = 0;
                playerAgent.destination = interactionInfo.point;
            }
        }
    }
}

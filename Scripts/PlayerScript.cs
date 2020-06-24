using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera mainCamera;

    private bool turning;
    private Quaternion targetRot;

    void Start()
    {
        mainCamera = Camera.main;

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        UpdateAnimator();

        if (Input.GetMouseButtonDown(0) && !Extensions.IsMouseOverUI())
            OnClick();

        if (turning && transform.rotation != targetRot)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 15f * Time.deltaTime);
        }
    }

    void OnClick()
    {
        Debug.Log("Left Clicked!");

        RaycastHit hit;
        Ray camToScreen = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camToScreen, out hit, Mathf.Infinity))
        {
            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    //if (!interactable.LookOnly)
                        MovePlayer(interactable.InteractPosition());

                    interactable.Interact(this);
                }
                else
                {
                    MovePlayer(hit.point);
                }
            }
        }
    }

    public bool CheckIfArrived()
    {
        return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }

    void MovePlayer(Vector3 targetPosition)
    {
        turning = false;

        agent.SetDestination(targetPosition);
        DialogSystem.Instance.HideDialog();
    }


    private void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("Velocity", speed);
    }


    public void SetDirection(Vector3 targetDirection)
    {
        turning = true;
        targetRot = Quaternion.LookRotation(targetDirection - transform.position);
    }
}

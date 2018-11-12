using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private PlayerMotor motor;
    private PlayerManager playerManager;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();

        playerManager = PlayerManager.instance;
        playerManager.isInteracting = false;
    }

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (!playerManager.isInteracting)
        {
            if (xInput != 0 || yInput != 0)
            {
                motor.Move(xInput, yInput);
                motor.Rotate(xInput, yInput);
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {

                    //Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red, 2.0f);
                    //Debug.Log(hitInfo.transform.name);
                    Interactable interactable;
                    if (hitInfo.transform.GetComponent<Interactable>())
                    {
                        Debug.Log("Hit an interactable");
                        interactable = hitInfo.transform.GetComponent<Interactable>();

                        if (interactable.canInteract)
                        {
                            interactable.Interact();
                        }
                    }
                    else
                    {
                        interactable = null;
                    }
                }
            }
        
        }
        
    }
}

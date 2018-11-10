using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private PlayerMotor motor;
    public bool isInteracting;

    private void Start()
    {
        isInteracting = false;
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (!isInteracting)
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
                    Interactable interactable = hitInfo.transform.GetComponent<Interactable>();
                    if (interactable.canInteract)
                    {
                        interactable.Interact();
                    }
                }
            }
        
        }
        
    }
}

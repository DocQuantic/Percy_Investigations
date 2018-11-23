using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {

    public float speed = 2.0f;

    private CameraController cameraController;
    private Vector3[] orientation;

    private void OnEnable()
    {
        Debug.Log("On");
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        orientation = cameraController.GetCameraOrientation();
    }

    private void OnDisable()
    {
        Debug.Log("Off");
        cameraController = null;
    }

    public void Move(float xInput, float yInput)
    {
        if (xInput != 0)
        {
            transform.Translate(transform.InverseTransformDirection(orientation[1]) * xInput * speed * Time.deltaTime);
        }

        if (yInput != 0)
        {
            transform.Translate(transform.InverseTransformDirection(orientation[0]) * yInput * speed * Time.deltaTime);
        }
    }

    public void Rotate(float xInput, float yInput)
    {
        Quaternion lookRotation = Quaternion.LookRotation((orientation[1] * xInput + orientation[0] * yInput).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
    }
}

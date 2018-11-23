using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCamera : MonoBehaviour {

    private Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.GetComponent<Transform>();
        mainCamera.position = transform.position;
        mainCamera.rotation = transform.rotation;
    }

}

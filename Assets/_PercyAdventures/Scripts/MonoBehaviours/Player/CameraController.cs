using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float fieldOfView;
    private Vector3 initTargetPos;
    private Vector3 cameraOffset;
    private Transform player;
    private PlayerManager playerManager;

    public Transform target;
    public float cameraFollowSpeed = 10.0f;
    public float zoomSpeed = 0.1f;
    public float minZoom = 1.0f;
    public float maxZoom = 60f;

    private void Start()
    {
        fieldOfView = Camera.main.fieldOfView;

        FindTargetInit();
        target.position = initTargetPos;

        playerManager = PlayerManager.instance;
        player = playerManager.GetPlayer();
    }
    
    private void Update()
    {
        if (!playerManager.isInteracting)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                Zoom();
            }
            UpdateTargetPosition();
        }
    }
    
    private void LateUpdate()
    {
        transform.LookAt(target);
        //UpdateCameraPosition();
    }
    

    public Vector3[] GetCameraOrientation()
    {
        Vector3[] orientation = new Vector3[2];
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;

        orientation[0] = camForward.normalized;
        orientation[1] = camRight.normalized;

        return orientation;
    }

    public void FindTargetInit()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit " + hit.transform.name);
            initTargetPos = hit.point;
            cameraOffset = hit.point-transform.position;
        }
    }

    private void Zoom()
    {
        fieldOfView = Mathf.Lerp(fieldOfView, fieldOfView - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime, zoomSpeed*Time.deltaTime);
        fieldOfView = Mathf.Clamp(fieldOfView, minZoom, maxZoom);
        Camera.main.fieldOfView = fieldOfView;
    }

    private void UpdateTargetPosition()
    {
        Vector3 position = (player.position+Vector3.up - initTargetPos) * (1 - (fieldOfView - minZoom) / (maxZoom - minZoom));
        Vector3 targetPos = target.position;

        targetPos.x = Mathf.Lerp(targetPos.x, position.x + initTargetPos.x, cameraFollowSpeed * Time.deltaTime);
        targetPos.y = Mathf.Lerp(targetPos.y, position.y + initTargetPos.y, cameraFollowSpeed * Time.deltaTime);
        targetPos.z = Mathf.Lerp(targetPos.z, position.z + initTargetPos.z, cameraFollowSpeed * Time.deltaTime);

        target.position = targetPos;
    }

    /*
    private void UpdateCameraPosition()
    {
        transform.position = target.position - cameraOffset;
    }
    */
}

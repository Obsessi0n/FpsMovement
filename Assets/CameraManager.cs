using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private float horizontalSensitivity = 2f;
    [SerializeField]
    private float verticalSensitivity = 2f;


    private Transform playerCamera;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    void Awake()
    {
        if(transform.Find("PlayerCamera") != null)
        {
            playerCamera = transform.Find("PlayerCamera");
        }
        else
        {
            Debug.LogError("Player Camera not found!");
        }
    }



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        GetMouseInput();
        MoveCamera();
    }


    void GetMouseInput()
    {
        yaw += horizontalSensitivity * Input.GetAxis("Mouse X");
        pitch -= verticalSensitivity * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90f, 90f);
    }

    void MoveCamera()
    {
        playerCamera.transform.eulerAngles = new Vector3(pitch, playerCamera.transform.eulerAngles.y, 0.0f);

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
    }
}

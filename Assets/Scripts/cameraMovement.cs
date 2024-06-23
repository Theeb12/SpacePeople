using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class cameraMovement : NetworkBehaviour 
{
    public float mouseSens = 2.0f;
    float inY;
    public float movementSpeed = 10f;
    float inX;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        inY += Input.GetAxis("Mouse Y") * mouseSens; // * mouseSens * Time.deltaTime;
        inY = Mathf.Clamp(inY, -90, 90);

        transform.localRotation = Quaternion.Euler(-inY, -inX, 0);
        //transform.localRotation = Quaternion.Euler

    }
}

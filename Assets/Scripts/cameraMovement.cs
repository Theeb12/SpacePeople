using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations;

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
    void Update() {
        inY += Input.GetAxis("Mouse Y") * mouseSens;
        inY = Mathf.Clamp(inY, -90, 90);
        // inX += Input.GetAxis("Mouse X") * mouseSens;
        // transform.localRotation = Quaternion.Euler(-inY, inX-transform.parent.rotation.eulerAngles.y, 0);
        transform.localRotation = Quaternion.Euler(-inY, 0, 0);

    }
}

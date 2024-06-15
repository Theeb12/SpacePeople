using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class cameraMovement : NetworkBehaviour 
{
    public float mouseSens = 2.0f;
    float cameraVert;
    float cameraHorizontal;
    float inX;
    float inY;
    public GameObject player;
    public float movementSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        inY += Input.GetAxis("Mouse Y") * mouseSens; // * mouseSens * Time.deltaTime;
        inY = Mathf.Clamp(inY, -90, 90);

        transform.localRotation = Quaternion.Euler(-inY, 0, 0);
        //Quaternion yQuat = Quaternion.AngleAxis(inY, Vector3.up);

        //Debug.Log(transform.rotation.x);
        //transform.Rotate(new Vector3(-inY, 0, 0), Space.Self);

        
        //transform.Rotate(new Vector3(1, 0, 0));
    }
}

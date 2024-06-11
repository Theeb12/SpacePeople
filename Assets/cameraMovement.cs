using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
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
        
    }

    // Update is called once per frame
    void Update()
    {
        inY = Input.GetAxis("Mouse X") * mouseSens; // * mouseSens * Time.deltaTime;
        inX = -Input.GetAxis("Mouse Y") * mouseSens; // * mouseSens * Time.deltaTime;

        transform.Rotate(new Vector3(inX, inY, 0), Space.World);
        if (Input.GetKey("w"))
        {
            player.transform.Translate(transform.forward * Time.deltaTime * movementSpeed);

        }
        //transform.Rotate(new Vector3(1, 0, 0));
    }
}

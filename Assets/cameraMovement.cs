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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inY += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        inX -= Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        transform.eulerAngles = new Vector3(inX, inY, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airlockDoor : MonoBehaviour
{
    public int state = 1;
    float rotSpeed = 50f;
    public bool closed = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(state == -1)
        {
            closed = false;
            openDoor();
        }
        else
        {
            closeDoor();   
        }
    }

    void openDoor()
    {
        if(transform.rotation.y >= -0.42f)
        {
            transform.Rotate(0, -Time.deltaTime * rotSpeed, 0);
        }
    }
    void closeDoor()
    {
        if(transform.rotation.y < 0.7071068f)
        {
            transform.Rotate(0, Time.deltaTime * rotSpeed, 0);
        }
        else
        {
            closed = true;
        }
    }
}

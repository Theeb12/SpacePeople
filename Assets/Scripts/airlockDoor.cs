using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airlockDoor : MonoBehaviour
{
    public bool doorClosing = true;
    float rotSpeed = 50f;
    public bool closed = true;
    void Update(){
        if(!doorClosing){
            //open door
            closed = false;
            if(transform.rotation.y >= -0.42f){
                transform.Rotate(0, -Time.deltaTime * rotSpeed, 0);
            }
        }
        else{
            //close door
            if(transform.rotation.y < 0.7071068f){
                transform.Rotate(0, Time.deltaTime * rotSpeed, 0);
            }
            else{
                closed = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyMove: MonoBehaviour
{
    public Transform origin;
    Vector3 direction;
    float rotSpeed = 1;
    Quaternion lookRot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = (origin.position - transform.position);
        lookRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed) ;

    }
}

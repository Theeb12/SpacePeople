using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class FollowHand : MonoBehaviour{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (transform.parent != null) {
            transform.GetComponent<NetworkTransform>().enabled = false;
            transform.GetComponent<gravity>().useGrav = false;
            transform.position = transform.parent.Find("Main Camera").Find("pickupArea").position;
            transform.rotation = transform.parent.Find("Main Camera").Find("pickupArea").rotation;
        }
    }
}

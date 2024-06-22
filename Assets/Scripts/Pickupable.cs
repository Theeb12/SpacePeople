using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class FollowHand : MonoBehaviour{
    Transform prevParent = null;
    // Update is called once per frame
    void Update(){
        bool stateChange = transform.parent != prevParent;
        if (stateChange) {
            transform.GetComponent<NetworkTransform>().enabled = transform.parent == null;
        }
        if (transform.parent != null) {
            transform.position = transform.parent.Find("UpperBody").Find("Main Camera").Find("pickupArea").position;
            transform.rotation = transform.parent.Find("UpperBody").Find("Main Camera").Find("pickupArea").rotation;
        }
        prevParent = transform.parent;
    }
}

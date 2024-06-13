using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public float pickupDist = 1.0f;
    public LayerMask pickUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, pickupDist, pickUp))
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}

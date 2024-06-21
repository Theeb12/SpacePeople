using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    // Start is called before the first frame update
    float pickupDist = 5.0f;
    public LayerMask pickUp;
    public LayerMask door;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDist, door))
        {
            Debug.Log("dooooor");
            if (Input.GetKeyDown("e"))
            {
                hit.transform.gameObject.GetComponent<airlockDoor>().state *= -1;
            }
        }
    }
}

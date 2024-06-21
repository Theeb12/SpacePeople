using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    // Start is called before the first frame update
    float pickupDist = 5.0f;
    public LayerMask pickUp;
    public LayerMask door;
    public LayerMask button;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDist, door))
        {
            if (Input.GetKeyDown("e"))
            {
                hit.transform.gameObject.GetComponent<airlockDoor>().state *= -1;
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDist))
        {
            if (Input.GetKeyDown("e"))
            {
                if (hit.transform.gameObject.name == "EjectButton")
                {
                    hit.transform.gameObject.GetComponent<eject>().state = 1;
                }
            }
        }
    }
}

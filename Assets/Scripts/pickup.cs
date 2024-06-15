using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public float pickupDist = 1.0f;
    public LayerMask pickUp;
    bool isHolding = false;
    bool isThrowing = false;
    bool sameFrame = false;
    Rigidbody heldObjRb;
    gravity heldGrav;
    [SerializeField] Transform holdArea;

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
            if (Input.GetKeyDown("e") && !isHolding)
            {
                isHolding = true;
                heldObjRb = hit.transform.gameObject.GetComponent<Rigidbody>();
                heldGrav = hit.transform.gameObject.GetComponent<gravity>();
                heldGrav.gravScale = 0;
                heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;
                heldObjRb.transform.parent = holdArea;
                sameFrame = true;
            }
        }
        if (Input.GetKeyDown("e") && isHolding && !sameFrame)
        {
            isHolding = false;
            //heldObjRb.useGravity = true;
            heldGrav.gravScale = 10.0f;
            heldGrav = null;
            heldObjRb.constraints = RigidbodyConstraints.None;

            heldObjRb.transform.parent = null;
        }
        sameFrame = false;

        if (isHolding)
        {
            if(Vector3.Distance(heldObjRb.transform.position, holdArea.position) > 0.1f)
            {
                Vector3 moveDir = holdArea.position - heldObjRb.transform.position;
                heldObjRb.AddForce(moveDir * 150.0f);
            }
        }
    }
}

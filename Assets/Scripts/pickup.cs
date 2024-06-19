using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class pickup : NetworkBehaviour
{
    float throwTimer = 0f;
    public float pickupDist = 1.0f;
    public LayerMask pickUp;
    bool isHolding = false;
    bool isThrowing = false;
    bool sameFrame = false;
    GameObject heldObj;
    Rigidbody heldObjRb;
    gravity heldGrav;
    [SerializeField] Transform holdArea;
    public float throwStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!IsOwner) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDist, pickUp))
        {
            //Debug.Log(hit.transform.gameObject.name);
            if (Input.GetKeyDown("e") && !isHolding)
            {
                isHolding = true;
                heldObjRb = hit.transform.gameObject.GetComponent<Rigidbody>();
                heldGrav = hit.transform.gameObject.GetComponent<gravity>();
                heldGrav.useGrav = false;
                heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;
                // heldObjRb.transform.parent = holdArea;
                heldObj = hit.transform.gameObject;
                heldObjRb.drag = 10f;
                //heldObjRb.isKinematic = true;
                sameFrame = true;
            }
        }
        if (isThrowing && throwTimer <= 1.5f)
        {
            throwTimer += Time.deltaTime;
            Debug.Log(throwTimer);
        }
        if (Input.GetKeyDown("e") && isHolding && !sameFrame && !isThrowing)
        {
            isThrowing = true;
        }
        if (isThrowing && Input.GetKeyUp("e"))
        {
            //heldObjRb.isKinematic = false;
            isThrowing = false;
            isHolding = false;
            //heldObjRb.useGravity = true;
            // heldObjRb.transform.parent = null;
            heldObjRb.constraints = RigidbodyConstraints.None;
            heldObjRb.AddForce(transform.forward * throwStrength * throwTimer, ForceMode.Impulse);
            throwTimer = 0;
            heldGrav.useGrav = true;
            heldGrav = null;
            heldObjRb.drag = 1f;
            heldObj = null;
            heldObjRb = null;
        }
        sameFrame = false;

        if (isHolding)
        {
            if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
            {
                Vector3 moveDir = holdArea.position - heldObj.transform.position;
                heldObjRb.AddForce(moveDir * 500.0f);
                //transform.position = Vector3.MoveTowards(heldObj.transform.position, holdArea.position, Time.deltaTime * 2.0f);
            }
        }
    }
}

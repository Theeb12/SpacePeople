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

    [SerializeField] GameObject cube;



    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupDist, pickUp)) {
            if (Input.GetKeyDown("e") && !isHolding) {
                heldObj = Instantiate(cube, holdArea);
                heldObjRb = heldObj.GetComponent<Rigidbody>();
                heldGrav = heldObj.GetComponent<gravity>();
                heldGrav.useGrav = false;
                sameFrame=true;
                heldObjRb.isKinematic = true;
            }
        }
        if (isThrowing && throwTimer <= 1.5f) {
            throwTimer += Time.deltaTime;
        }
        if (Input.GetKeyDown("e") && isHolding && !sameFrame && !isThrowing) {
            isThrowing = true;
        }
        if (isThrowing && Input.GetKeyUp("e")) {
            isThrowing = false;
            isHolding = false;

            heldObjRb.AddForce(transform.forward * throwStrength * throwTimer, ForceMode.Impulse);

            heldGrav.useGrav = true;
            heldGrav = null;
            heldObjRb.drag = 1f;
            heldObj = null;
            heldObjRb = null;
            throwTimer = 0;
        }
        sameFrame = false;

        if (isHolding){
            heldObj.transform.position = holdArea.position;
            heldObj.transform.rotation = holdArea.rotation;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class pickup : NetworkBehaviour{
    float throwTimer = 0f;
    public float pickupDist = 1.0f;
    public LayerMask pickUp;
    bool isHolding = false;
    bool isThrowing = false;
    [SerializeField] Transform holdArea;
    public float throwStrength;

    [SerializeField] GameObject playerCamera;

    ulong targetObjectID;
    bool isKeyReleased = true;


    // Update is called once per frame
    void Update() {
        if(!IsOwner) return;
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupDist, pickUp)) {
            if (Input.GetKeyDown("e") && !isHolding) {
                targetObjectID = hit.transform.gameObject.GetComponent<NetworkObject>().NetworkObjectId;
                PickupObjectServerRpc(this.NetworkObjectId, targetObjectID);
                isHolding = true;
                isKeyReleased = false;
            }
        }
        if (Input.GetKey("e") && isHolding && throwTimer <= 1.5f && isKeyReleased) {
            throwTimer += Time.deltaTime;
            isThrowing = true;

        }
        if (Input.GetKeyUp("e")) {
            isKeyReleased = true;
            if (isHolding && isThrowing) {
                ThrowObjectServerRpc(targetObjectID, playerCamera.transform.forward * throwStrength * throwTimer);
                isHolding = false;
                isThrowing = false;
                throwTimer = 0;
            }
        }
    }

    [ServerRpc]
    private void PickupObjectServerRpc(ulong playerID, ulong targetObjectID) {
        NetworkObject targetObject = NetworkManager.SpawnManager.SpawnedObjects[targetObjectID];
        NetworkObject player = NetworkManager.SpawnManager.SpawnedObjects[playerID];
        targetObject.transform.parent = player.transform;
    }

    [ServerRpc]
    private void ThrowObjectServerRpc(ulong targetObjectID, Vector3 throwForce) {
        NetworkObject targetObject = NetworkManager.SpawnManager.SpawnedObjects[targetObjectID];
        Rigidbody objectRB = targetObject.GetComponent<Rigidbody>();

        targetObject.transform.parent = null;
        objectRB.velocity = Vector3.zero;
        objectRB.AddForce(throwForce, ForceMode.Impulse);
    }
}

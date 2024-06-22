using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class interact : NetworkBehaviour{
    float pickupDist = 5.0f;
    public LayerMask pickUp;
    public LayerMask door;
    public LayerMask button;

    [SerializeField] GameObject MainCamera;

    void Update(){
        RaycastHit hit;
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, pickupDist, door)){
            if (Input.GetKeyDown("e")) {
                ToggleDoorServerRpc(hit.transform.gameObject.GetComponent<NetworkObject>().NetworkObjectId);
            }
        }
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, pickupDist, button)){
            if (Input.GetKeyDown("e")){
                EjectButtonServerRpc(hit.transform.gameObject.GetComponent<NetworkObject>().NetworkObjectId);
            }
        }
    }

    [ServerRpc]
    private void ToggleDoorServerRpc(ulong targetObjectID){
        NetworkObject targetObject = NetworkManager.SpawnManager.SpawnedObjects[targetObjectID];
        targetObject.GetComponent<airlockDoor>().doorClosing ^= true;
    }

    [ServerRpc]
    private void EjectButtonServerRpc(ulong targetObjectID){
        NetworkObject targetObject = NetworkManager.SpawnManager.SpawnedObjects[targetObjectID];
        targetObject.GetComponent<eject>().state = 1;
    }
}

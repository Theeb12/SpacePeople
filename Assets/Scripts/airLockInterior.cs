using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class airLockInterior : NetworkBehaviour{
    public int state = 0;
    float ejectStrength = 100.0f;
    private void OnTriggerStay(Collider col) {
        if(state == 1 && col.gameObject.GetComponent<gravity>().enabled == true){
            if (col.gameObject.CompareTag("Player")){
                EjectPlayerClientRpc(col.gameObject.GetComponent<NetworkObject>().NetworkObjectId);
            } else {
                Debug.Log("ejecting " + col.gameObject.name);
                col.gameObject.GetComponent<gravity>().useGrav = false;
                col.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * ejectStrength * col.gameObject.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
            }
        }
    }

    [ClientRpc]
    public void EjectPlayerClientRpc(ulong playerObjectID){
        NetworkObject player = NetworkManager.Singleton.SpawnManager.SpawnedObjects[playerObjectID];
        Debug.Log(playerObjectID);
        Debug.Log(NetworkManager.Singleton.LocalClientId);
        Debug.Log("ejecting player");
        player.gameObject.GetComponent<gravity>().useGrav = false;
        player.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * ejectStrength * player.gameObject.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
    }
}

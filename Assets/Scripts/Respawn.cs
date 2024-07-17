using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    [SerializeField] float maxDist = 400;
    GameObject[] spawnPoints;
    void Start()
    {
        // get all possible spawn locations
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
    }

    void Update()
    {
        // teleport player to spawn room if distance from center exceeds maxDist and the respawn points are empty

        if(Vector3.Distance(Vector3.zero, transform.position) > maxDist)
        {
            foreach (GameObject i in spawnPoints)
            {
                if (i.GetComponent<PlayerSpawner>().isEmpty)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    transform.rotation = i.transform.rotation;
                    transform.position = i.transform.position;

                }
            }
        }
    }
}

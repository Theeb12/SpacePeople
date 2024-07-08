using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float maxDist = 400;
    GameObject[] spawnPoints;
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Vector3.zero, transform.position) > maxDist)
        {
            //Debug.Log("doing the thing");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public float gravScale = 10f;
    Rigidbody rb;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravScale = rb.mass * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        direction = new Vector3(0, transform.position.y, transform.position.z);
        rb.AddForce(direction * gravScale);
    }
}

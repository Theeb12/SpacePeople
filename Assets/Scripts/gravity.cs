using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    float gravScale = 10f;
    public bool useGrav = true;
    Rigidbody rb;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravScale = rb.mass * 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (useGrav)
        {
            direction = new Vector3(0, transform.position.y, transform.position.z);
            rb.AddForce(direction * gravScale);
        }
    }
}

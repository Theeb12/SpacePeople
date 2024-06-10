using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyMove: MonoBehaviour
{
    public Transform origin;
    Vector3 difference;
    Vector3 direction;
    float rotSpeed = 1;
    Quaternion lookRot;
    Rigidbody rb;
    public float gravScale = 1;
    public float jumpForce;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(origin);
        direction = new Vector3(0, transform.position.y, transform.position.z);
        rb.AddForce(direction * gravScale);

        if (Input.GetKey("w"))
        {

        }
        if (Input.GetKeyDown("space"))
            rb.AddForce(transform.up * jumpForce);
    }
}

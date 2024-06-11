using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDummy : MonoBehaviour
{
    bool canJump;
    public float mouseSens;
    float inX;
    Vector3 direction;
    public float gravScale = 1;
    Rigidbody rb;
    Vector3 lookDir;
    Vector3 offset;
    public float movementSpeed;
    public float jumpForce = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //inX = 0;
        inX += Input.GetAxis("Mouse X") * mouseSens;
        Quaternion xQuat = Quaternion.AngleAxis(inX, Vector3.up);

        //lookDir = Vector3.zero - transform.position;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.up, 50.0f);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.CompareTag("Floor"))
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hits[i].normal) * xQuat;
            }
        }

        //transform.rotation = Quaternion.FromToRotation(Vector3.up, lookDir) * xQuat;
        Vector3 movDir = new Vector3(0,0,0);
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h * movementSpeed, 0, v * movementSpeed);
        transform.Translate(move * Time.deltaTime);

        
        if (Input.GetKey("space") && canJump)
        {
            //Debug.Log("doing the thing");
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }

        direction = new Vector3(0, transform.position.y, transform.position.z);
        rb.AddForce(direction * gravScale);
    }
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("collided");
        canJump = true;
    }
}

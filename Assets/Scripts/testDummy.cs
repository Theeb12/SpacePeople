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
    float h;
    float v;

    public float groundDrag;
    public LayerMask whatIsGround;
    bool readyToJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        canJump = Physics.Raycast(transform.position, -transform.up, 2.75f * 0.5f + 0.2f, whatIsGround);
        inX += Input.GetAxis("Mouse X") * mouseSens;
        Quaternion xQuat = Quaternion.AngleAxis(inX, Vector3.up);

        //lookDir = Vector3.zero - transform.position;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.up, 50.0f, whatIsGround);
        for(int i = 0; i < hits.Length; i++)
        {
                //Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hits[i].normal) * xQuat, Time.deltaTime * 500);
                //transform.rotation = rot;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hits[i].normal) * xQuat;
        }

        //transform.rotation = Quaternion.FromToRotation(Vector3.up, lookDir) * xQuat;
        Vector3 movDir = new Vector3(0,0,0);

        if (canJump)
        {
            rb.drag = groundDrag;
            if (Input.GetKey("space") && canJump)
            {
                //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }
        else
        {
            rb.drag = groundDrag;
        }
        

        direction = new Vector3(0, transform.position.y, transform.position.z);
        rb.AddForce(direction * gravScale);
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

    }
    private void FixedUpdate()
    {
        if (canJump)
        {
            Vector3 move = transform.forward * v + transform.right * h;
            rb.AddForce(move.normalized * movementSpeed, ForceMode.Force);
            //transform.Translate(move * Time.deltaTime);
        }
        else
        {
            Vector3 move = transform.forward * v + transform.right * h;
            rb.AddForce(move.normalized * movementSpeed * 0.2f, ForceMode.Force);
            //transform.Translate(move * Time.deltaTime);
        }
        if (h == 0 && v == 0)
        {
            rb.velocity /= 1.2f;
        }

    }
    private void OnCollisionEnter(Collision col)
    {

    }
}

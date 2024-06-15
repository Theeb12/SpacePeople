using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class testDummy : NetworkBehaviour {
    bool canJump;
    public float mouseSens;
    float inX;
    Vector3 direction;
    public float gravScale = 1;
    Rigidbody rb;
    public float movementSpeed;
    public float jumpForce = 5;
    float h;
    float v;

    public float groundDrag;
    public LayerMask whatIsGround;

    public GameObject cameraHolder;

    // Start is called before the first frame update
    void Start() {
        if (!IsOwner) return;
        rb = GetComponent<Rigidbody>();

        rb.drag = groundDrag;
        //enable camera
        cameraHolder.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        if (!IsOwner) return; // only control player object
        canJump = Physics.Raycast(transform.position, -transform.up, 2.75f * 0.5f + 0.01f, whatIsGround);


        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        inX += Input.GetAxis("Mouse X") * mouseSens;
    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;
        // rotate object x axis with mouse and rotate object to match ground normal
        Quaternion xQuat = Quaternion.AngleAxis(inX, Vector3.up);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.up, 50.0f, whatIsGround);
        for(int i = 0; i < hits.Length; i++) {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hits[i].normal) * xQuat;
        }
        // movement, add drag if we're in the air so we can't fly
        Vector3 move = transform.forward * v + transform.right * h;
        float airDrag = canJump ? 1 : 0.2f;
        rb.AddForce(move.normalized * movementSpeed * airDrag, ForceMode.Force);

        // jump
        if (canJump) {
            if (Input.GetKey("space") && canJump) {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }

        // slow down if we're not pressing anything
        if (h == 0 && v == 0) {
            rb.velocity /= 1.2f;
        }

        // gravity
        direction = new Vector3(0, transform.position.y, transform.position.z);
        rb.AddForce(direction * gravScale);
        
        
    }
}

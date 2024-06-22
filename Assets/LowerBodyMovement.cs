using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LowerBodyMovement : NetworkBehaviour
{
    bool canJump;
    Rigidbody rb;
    public float movementSpeed;
    public float jumpForce = 5;
    float h;
    float v;
    public float groundDrag;
    public LayerMask whatIsGround;

    public GameObject cameraHolder;
    [SerializeField] GameObject UpperBody;

    Vector3 upDir;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;
        rb = GetComponent<Rigidbody>();

        rb.drag = groundDrag;
        //enable camera
        cameraHolder.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return; // only control player object

        canJump = Physics.Raycast(transform.position, -UpperBody.transform.up, 3f * 0.5f + 0.01f, whatIsGround);

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // rotate object x axis with mouse and rotate object to match ground normal
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -UpperBody.transform.up, 50.0f, whatIsGround);
        for (int i = 0; i < hits.Length; i++)
        {
            upDir = hits[i].normal;
        }

    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;

        // movement, add drag if we're in the air so we can't fly
        Vector3 move = UpperBody.transform.forward * v + UpperBody.transform.right * h;
        float airDrag = canJump ? 1 : 0.2f;
        rb.AddForce(move.normalized * movementSpeed * airDrag, ForceMode.Force);

        // jump
        if (canJump)
        {
            if (Input.GetKey("space") && canJump)
            {
                rb.AddForce(upDir * jumpForce, ForceMode.Impulse);
            }
        }

        // slow down if we're not pressing anything
        if (h == 0 && v == 0)
        {
            rb.velocity /= 1.2f;
        }
    }
}

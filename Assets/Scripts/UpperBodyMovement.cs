using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UpperBodyMovement : NetworkBehaviour
{
    [SerializeField] GameObject lowerBody;
    [SerializeField] GameObject upperBody;
    float movSpeed = 30;
    // float rotSpeed = 100;
    public float mouseSens;
    float h;
    float inX;
    public LayerMask whatIsGround;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return; // only control player object

        h = Input.GetAxisRaw("Horizontal");
        inX += Input.GetAxis("Mouse X") * mouseSens;

        Vector3 target = lowerBody.transform.position + upperBody.transform.up;
        upperBody.transform.position = Vector3.MoveTowards(upperBody.transform.position, target, Time.deltaTime * movSpeed);


        Quaternion xQuat = Quaternion.AngleAxis(inX, Vector3.up);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(upperBody.transform.position, -upperBody.transform.up, 50.0f, whatIsGround);
        for (int i = 0; i < hits.Length; i++)
        {
            upperBody.transform.rotation = Quaternion.FromToRotation(Vector3.up, hits[i].normal) * xQuat;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour {
    float mouseSens = 2f;
    float inX;
    Rigidbody rb;
    float movementSpeed = 25;
    float jumpForce = 500;
    float h;
    float v;

    public LayerMask groundMask;

    public GameObject cameraHolder;

    bool jumping = false;

    void Start() {
        if (!IsOwner) return;
        rb = GetComponent<Rigidbody>();
        cameraHolder.SetActive(true);
        transform.position = new Vector3(0, -28, 0);
    }

    void Update() {
        if (!IsOwner) return; // only control player object

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        inX = Input.GetAxis("Mouse X") * mouseSens;

        Quaternion xQuat = Quaternion.AngleAxis(inX, Vector3.up);
        transform.rotation*=xQuat;
    }
    private void FixedUpdate() {
        if (!IsOwner) return;

        /* Ground Detection
        Because we are moving on a curved surface, our ground detection needs to go in all directions
        and return the closest point, not just the point below us. 
        */
        float minDistance = 1000;
        bool groundDetected = false;
        RaycastHit ground = new RaycastHit();
        foreach (var direction in GetSphereDirections(1000)){
            Debug.DrawRay(transform.position, direction*5f, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 3f, groundMask)){
                if (hit.distance < minDistance){
                    groundDetected = true;
                    minDistance = hit.distance;
                    ground = hit;
                }
            }
        }
        // draw ground detection
        Debug.DrawRay(transform.position, -ground.normal * 5f, Color.blue);


        /* Rotation Force
        rotate using a hacky damp spring calculation to smooth out the rotation. Goal is to stay
        perpendicular to the detected ground.
        */
        if(groundDetected) {
            Quaternion target = Quaternion.FromToRotation(transform.up, ground.normal);
            Vector3 rotationAxis;
            float rotationDegrees;
            target.ToAngleAxis(out rotationDegrees, out rotationAxis);
            rotationAxis.Normalize();
            float rotationRadians = rotationDegrees * Mathf.Deg2Rad;
            // first constant is force, second is damping
            rb.AddTorque(rotationAxis * rotationRadians * rb.mass * 120 - (rb.angularVelocity * 180)); 
        }
        
        /* Hover Force
            To prevent player from hitting the ground and creating a jerky motion, hover the 
            player using a damp spring calculation.
        */
        if (groundDetected && !jumping) {
            // constants for tuning
            float dampFactor = 1;
            float dampFrequency = 30;
            float targetHoverHeight = 2f;

            // equation variables
            Vector3 forceDirection = -ground.normal;
            float springStrength = rb.mass * dampFrequency * dampFrequency;
            float dampStrength = dampFactor * 2 * rb.mass * dampFrequency;
            float distanceToTargetRideHeight = ground.distance - targetHoverHeight;
            float relativeVelocity = Vector3.Dot(rb.velocity, forceDirection);
            // if the ground is a rigid body with velocity, we would need to 
            // subtract it from here to get a true relative velocity
            // - Vector3.dot(rayDirection, ground.rb.velocity)

            // spring force
            float tension = distanceToTargetRideHeight * springStrength;
            float damp = relativeVelocity * dampStrength;
            float magnitude = tension - damp;
            Vector3 springForce = forceDirection * magnitude;
            Debug.DrawRay(transform.position, springForce, Color.green);
            rb.AddForce(springForce);
        }

        
        /* Movement
        Technically we should move the player in a perpendicular direction to the detected ground
        but this seems to work fine for now. Limit velocity to max speed
        */
        Vector3 moveDirection = (transform.forward * v + transform.right * h).normalized;
        Vector3 maxVelocity = moveDirection * movementSpeed;
        Vector3 acceleration = (maxVelocity - rb.velocity)/ .1f; 
        rb.AddForce(acceleration, ForceMode.Force);

        // jump
        if (Input.GetKey("space") && groundDetected && !jumping) {
            jumping = true;
            rb.AddForce(ground.normal * jumpForce, ForceMode.Impulse);
            Invoke("endJump", .5f);
        }

        // slow down if we're not pressing anything
        if (h == 0 && v == 0 && groundDetected) {
            rb.velocity /= 1.5f;
        }        
    }

    void endJump() {
        jumping = false;
    }
    private Vector3[] GetSphereDirections(int numDirections) {
        var pts = new Vector3[numDirections];
        var inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        var off = 2f / numDirections;
 
        foreach (var k in System.Linq.Enumerable.Range(0, numDirections)) {
            var y = k * off - 1 + (off / 2);
            var r = Mathf.Sqrt(1 - y * y);
            var phi = k * inc;
            var x = (float) (Mathf.Cos(phi) * r);
            var z = (float) (Mathf.Sin(phi) * r);
            pts[k] = new Vector3(x, y, z);
        }
 
        return pts;
    }
}

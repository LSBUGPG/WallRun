using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float jumpForce = 1.0f;
    public float moveForce = 1.0f;
    public float liftForce = 1.0f;
    Rigidbody physics;
    int groundCount;
    int wallCount;
    float vertical;
    float horizontal;
    float lift;

    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // on ground
        if (groundCount > 0)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            lift = 0.0f;
            if (Input.GetButtonDown("Jump"))
            {
                physics.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        // wall running
        else if (wallCount > 0)
        {
            float forward = Mathf.Clamp01(Vector3.Dot(physics.velocity, transform.forward));
            vertical = Input.GetAxis("Vertical") * forward;
            horizontal = 0.0f;
            lift = liftForce * vertical;
        }
        // falling
        else
        {
            vertical = 0.0f;
            horizontal = 0.0f;
            lift = 0.0f;
        }
    }

    void FixedUpdate()
    {
        physics.AddForce(transform.right * horizontal * moveForce);
        physics.AddForce(Vector3.up * lift);
        physics.AddForce(transform.forward * vertical * moveForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            groundCount++;
        }
        if (collision.collider.CompareTag("Wall"))
        {
            wallCount++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            groundCount--;
        }
        if (collision.collider.CompareTag("Wall"))
        {
            wallCount--;
        }
    }
}

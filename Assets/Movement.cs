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
        // on surface
        if (groundCount + wallCount > 0)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            lift = liftForce;
            if (Input.GetButtonDown("Jump"))
            {
                physics.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        // falling
        else
        {
            vertical = 0.0f;
            horizontal = 0.0f;
            lift = 0.0f;
        }

        if (wallCount > 0 && groundCount == 0)
        {
            Debug.Log("Wall running");
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

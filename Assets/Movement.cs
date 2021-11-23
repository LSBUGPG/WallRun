using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float jumpForce = 1.0f;
    public float moveForce = 1.0f;
    public float liftForce = 1.0f;
    Rigidbody physics;
    int surfaceCount;
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
        if (surfaceCount > 0)
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
    }

    void FixedUpdate()
    {
        physics.AddForce(transform.right * horizontal * moveForce);
        physics.AddForce(Vector3.up * lift);
        physics.AddForce(transform.forward * vertical * moveForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Surface"))
        {
            surfaceCount++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Surface"))
        {
            surfaceCount--;
        }
    }
}

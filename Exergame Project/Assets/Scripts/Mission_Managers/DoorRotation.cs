using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    public float maxRotationAngle = 90f; // Maximum rotation angle in degrees
    public float rotationSpeed = 10f; // Speed factor for door rotation

    public Transform rightHand; // Reference to the right hand position

    private Rigidbody rb;
    private float initialRotationY;
    private bool isPushing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotationY = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        // Check if the right hand is in front of the door
        if (rightHand.position.z < transform.position.z)
        {
            isPushing = true;
        }
        else
        {
            isPushing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isPushing)
        {
            // Calculate the rotation based on the position of the right hand
            float rotation = -(transform.position.x - rightHand.position.x) * rotationSpeed;

            // Map the rotation to the desired angle within the limits
            float targetRotation = Mathf.Clamp(initialRotationY + rotation, initialRotationY, initialRotationY + maxRotationAngle);

            // Calculate the torque needed to achieve the target rotation
            float torque = (targetRotation - transform.rotation.eulerAngles.y) * rb.mass;

            // Apply the torque to the door's Rigidbody
            rb.AddRelativeTorque(Vector3.up * torque);

            
        }
    }
}
    


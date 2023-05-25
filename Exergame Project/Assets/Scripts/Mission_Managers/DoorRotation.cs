using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    public float rotationForce = 10f;  // Adjust this value to control the strength of the rotation
    private Rigidbody doorRigidbody;

    private void Start()
    {
        doorRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyRotation();
        }
    }

    private void ApplyRotation()
    {
        Vector3 rotationVector = transform.up * rotationForce;
        doorRigidbody.AddTorque(rotationVector, ForceMode.Impulse);
    }
}

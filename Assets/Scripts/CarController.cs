using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    private WheelController[] wheels;

    [SerializeField] private float motorTorque;
    [SerializeField] private float brakeTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float centerOfMassOffset;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        var centerOfMass = rb.centerOfMass;
        centerOfMass.y += centerOfMassOffset;
        rb.centerOfMass = centerOfMass;
        
        wheels = GetComponentsInChildren<WheelController>();
    }

    private void FixedUpdate()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        bool isAccelerating = vertical >= 0;
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.steerAngle = (wheel.steering) ? horizontal * maxSteeringAngle : 0f;
            wheel.wheelCollider.motorTorque = (isAccelerating && wheel.driving) ? vertical * motorTorque : 0f;
            wheel.wheelCollider.brakeTorque = isAccelerating ? 0 : brakeTorque;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    private WheelController[] wheels;

    public float steerAmount;
    public float brakeAmount;
    public float accelerationAmount;

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
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.steerAngle = (wheel.steering) ? steerAmount * maxSteeringAngle : 0f;
            wheel.wheelCollider.motorTorque = (wheel.driving) ? accelerationAmount * motorTorque : 0f;
            wheel.wheelCollider.brakeTorque = brakeAmount * brakeTorque;
        }
    }
}

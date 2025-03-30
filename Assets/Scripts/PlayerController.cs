using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CustomWheelCollider frontLeftWheel;
    [SerializeField] private CustomWheelCollider frontRightWheel;
    [SerializeField] private CustomWheelCollider backLeftWheel;
    [SerializeField] private CustomWheelCollider backRightWheel;
    
    [SerializeField] private float motorTorque;
    [SerializeField] private float brakeTorque;

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        
        frontLeftWheel.steerAngle = horizontal * 35f; 
        frontRightWheel.steerAngle = horizontal * 35f;

        backLeftWheel.motorTorque = (Input.GetKey(KeyCode.W)) ? motorTorque : 0f;
        backRightWheel.motorTorque = (Input.GetKey(KeyCode.W)) ? motorTorque : 0f;

        backLeftWheel.isBraking = Input.GetKey(KeyCode.S);
        backRightWheel.isBraking = Input.GetKey(KeyCode.S);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWheelCollider : MonoBehaviour
{
    [HideInInspector] public bool isBraking;
    [HideInInspector] public float steerAngle;
    [HideInInspector] public float motorTorque;
    
    private Rigidbody _rigidbody;

    [SerializeField] private float wheelRadius;
    [SerializeField] private float springForce;
    [SerializeField] private float dampingForce;
    [SerializeField] private float brakingForce;
    [SerializeField] private float lateralFriction;
    [SerializeField] private float springRestDistance;
    
    [SerializeField] private bool isDrivingWheel;
    [SerializeField] private bool isSteeringWheel;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        var springDir = transform.up;
        Vector3 point = transform.position;
        var pointVelocity = _rigidbody.GetPointVelocity(transform.position);
        Debug.DrawRay(transform.position, -springDir * springRestDistance * 2f, Color.red);
        if (Physics.Raycast(transform.position, -springDir, out RaycastHit hit, springRestDistance * 2f))
        {
            point = hit.point;
            var velocity = Vector3.Dot(springDir, pointVelocity);
            var offset = springRestDistance - hit.distance;
            var force = (offset * springForce) - (velocity * dampingForce);
            _rigidbody.AddForceAtPosition(springDir * force, point, ForceMode.Acceleration); 
        }

        if (isDrivingWheel)
        {
            var forwardForce = motorTorque / wheelRadius;
            _rigidbody.AddForceAtPosition(transform.forward * forwardForce, point, ForceMode.Acceleration);
        }
        
        var horizontalVelocity = new Vector3(pointVelocity.x, 0f, pointVelocity.z);
        if (isBraking)
        {
            var brakeForce = 0.8f * Vector3.Dot(horizontalVelocity, -transform.forward);
            _rigidbody.AddForceAtPosition(transform.forward * brakeForce, point, ForceMode.Acceleration);

            if (_rigidbody.velocity.magnitude < 0.1f)
            {
                _rigidbody.velocity = Vector3.zero; 
            }
        }

        if (isSteeringWheel)
        {
            var rotation = transform.localRotation.eulerAngles;
            rotation.y = steerAngle;
            transform.localRotation = Quaternion.Euler(rotation);
        }
        
        var lateralCorrection = lateralFriction * Vector3.Dot(horizontalVelocity, transform.right);  
        _rigidbody.AddForceAtPosition(transform.right * -lateralCorrection, point, ForceMode.Acceleration);
    }
}

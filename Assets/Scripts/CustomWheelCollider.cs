using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWheelCollider : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private float springForce;
    [SerializeField] private float dampingForce;
    [SerializeField] private float springRestDistance;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, springRestDistance * 2f))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.red);

            var velocity = new Vector3(0f, rb.GetPointVelocity(transform.position).y, 0f);
            var offset = springRestDistance - hit.distance;
            var spring = springForce * offset * Vector3.up;
            var damper = velocity * dampingForce; 
            var force = spring - damper; 
            rb.AddForceAtPosition(force, transform.position, ForceMode.Acceleration); 
        }
    }
}

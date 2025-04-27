using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public delegate void OnLeaveTrack(int id);
    public delegate void OnEnterTrack(int id);
    public static event OnLeaveTrack onLeaveTrack;
    public static event OnEnterTrack onEnterTrack;
    
    [SerializeField] public bool driving;
    [SerializeField] public bool steering;
    [SerializeField] private Transform wheelTransform;
    
    [HideInInspector] public WheelCollider wheelCollider;

    private int id;
    private bool offTrack;
    private Vector3 position;
    private Quaternion rotation;
    
    private void Start()
    {
        id = transform.GetSiblingIndex(); 
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void Update()
    {
        wheelCollider.GetWorldPose(out position, out rotation);    
        wheelTransform.transform.position = position;
        wheelTransform.transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        var layer = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layer))
        {
            if (hit.collider.gameObject.CompareTag("Grass") && !offTrack)
            {
                offTrack = true;
                onLeaveTrack?.Invoke(id);
            }

            if (hit.collider.gameObject.CompareTag("Road") && offTrack)
            {
                offTrack = false;
                onEnterTrack?.Invoke(id);
            }
        }
    }
}

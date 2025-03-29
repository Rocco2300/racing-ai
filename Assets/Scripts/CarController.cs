using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private List<CustomWheelCollider> wheels = new();
    
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            wheels.Add(transform.GetChild(i).GetComponent<CustomWheelCollider>());
        }     
    }
}

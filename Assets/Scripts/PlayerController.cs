using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CarController carController; 
    
    private void Start()
    {
        carController = GetComponent<CarController>();
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        carController.accelerationAmount = (Mathf.Sign(vertical) > 0) ? vertical : 0f;
        carController.brakeAmount = (Mathf.Sign(vertical) < 0) ? Mathf.Abs(vertical) : 0f;
        carController.steerAmount = horizontal;
    }
}

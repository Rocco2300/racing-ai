using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public delegate void OnCheckpointTriggered(int id);
    public static event OnCheckpointTriggered onCheckpointTriggered;

    public int id;

    private void OnTriggerEnter(Collider other)
    {
        onCheckpointTriggered?.Invoke(id);
    }
}

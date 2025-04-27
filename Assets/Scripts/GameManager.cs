using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject road;
    [SerializeField] private GameObject checkpointPrefab;
    
    private int currentCheckpoint = 0;
    private List<int> checkpoints = new();

    private void Awake()
    {
        int id = 1;
        var spline = road.GetComponent<SplineContainer>().Splines[0];
        foreach (var knot in spline.Knots)
        {
            var checkpoint = Instantiate(checkpointPrefab);         
            var knotPosition = new Vector3(knot.Position.x, knot.Position.y, knot.Position.z);
            var knotRotation = knot.Rotation * Quaternion.Euler(0, 90f, 0);
            checkpoint.transform.position = road.transform.position + knotPosition;
            checkpoint.transform.rotation = knotRotation;

            checkpoints.Add(id);
            checkpoint.GetComponent<CheckpointController>().id = id;
            id++;
        }
    }

    private void OnEnable()
    {
        WheelController.onLeaveTrack += OnLeaveTrack;
        WheelController.onEnterTrack += OnEnterTrack;
        CheckpointController.onCheckpointTriggered += OnCheckpointTriggered;
    }

    private void OnDisable()
    {
        WheelController.onLeaveTrack -= OnLeaveTrack;
        WheelController.onEnterTrack -= OnEnterTrack;
        CheckpointController.onCheckpointTriggered -= OnCheckpointTriggered;
    }

    private void OnLeaveTrack(int id)
    {
        Debug.Log("Out of track"); 
    }

    private void OnEnterTrack(int id)
    {
        Debug.Log("Back on track"); 
    }

    private void OnCheckpointTriggered(int id)
    {
        if (id == currentCheckpoint)
        {
            return;
        }

        if (id == currentCheckpoint + 1)
        {
            currentCheckpoint++;
            Debug.Log("Reached checkpoint");

            if (currentCheckpoint == checkpoints.Last())
            {
                currentCheckpoint = 0;
            }
        }
    }
}

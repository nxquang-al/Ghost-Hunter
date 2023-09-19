using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    private void Start()
    {
        // Get the offset between the target and the camera
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // Update a new position for the camera
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}

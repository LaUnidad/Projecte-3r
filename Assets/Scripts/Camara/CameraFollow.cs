using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform playerTransform;

    private Vector3 cameraOffset;

    [Range(0.01f, 1f)]
    public float smoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = playerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if (LookAtPlayer) transform.LookAt(playerTransform);
    }
}

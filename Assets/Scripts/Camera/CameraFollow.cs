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
    public bool rotateAroundPlayer = false;

    public float rotationSpeed = 5f;

    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (rotateAroundPlayer)
        {
            Quaternion camTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
             Quaternion camTurnAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.right);

            //Quaternion camTurnAgnle = Quaternion.Angle(camTurnAngleX, camTurnAngleY);

            cameraOffset = camTurnAngleX * cameraOffset;
        }

        //cameraOffset.magnitude += Input.GetAxis("Mouse ScrollWheel");
        //cameraOffset = Mathf.Clamp(ve)


        Vector3 newPos = playerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        

        if (Input.GetMouseButton(1))
        {
            cameraOffset = transform.position - playerTransform.position;
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
            //if(mouseScroll <=)
            transform.position = transform.position += (cameraOffset * mouseScroll);

            
            Debug.LogWarning("MouseScroll: " + mouseScroll);
        }

    
        if (LookAtPlayer || rotateAroundPlayer) transform.LookAt(playerTransform);

         //Vector3 newRotation = Vector3.RotateTowards(transform.forward, -cameraOffset, rotationSpeed * Time.deltaTime, 0f);

        //transform.rotation = Quaternion.LookRotation(newRotation);
       
        Debug.Log("CameraOffset : " + cameraOffset);
    }
    /*
    public void CameraZoom(Vector3 cameraOffset, float radius)
    {
        cameraOffset.magnitude
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 offset;
    Transform target;

    [Range (0,1)] public float lerpValue;

    public float sensibilidad;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(transform.position, target.position +offset, lerpValue);
        offset =  Quaternion.AngleAxis(Input.GetAxis("Mouse X")* sensibilidad, Vector3.up) * offset;
        transform.LookAt(target);
    }
}

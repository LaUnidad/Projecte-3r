using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLeavesRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity;
    public GameObject rotatePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(rotatePoint.transform.position,Vector3.up ,velocity*Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameRotationAs : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject LivePlant;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(LivePlant);
        this.transform.rotation = LivePlant.transform.rotation;
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "LivePlant")
        {
            LivePlant = other.gameObject;
        }
    }


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "LivePlant")
        {
            LivePlant = other.gameObject;
        }
    }
}

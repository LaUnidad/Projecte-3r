using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoor : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "AspirableObject" && other.gameObject.GetComponent<AspirableObject>().IAmMagnetic == true)
        {
            //if(other.gameObject.GetComponent<AspirableObject>().rgbd.velocity.y >= 1)
            //{
                Destroy(this.gameObject);
            //}
        }
    }
}

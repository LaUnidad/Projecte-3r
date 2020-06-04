using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (BoxCollider))]
public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider boxy;

    public bool Exploted;
    void Start()
    {
        boxy = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "AspirableObject" && other.gameObject.GetComponent<AspirableObject>().IAmMagnetic)
        {
            ExploteYourChildren();
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "AspirableObject" && other.gameObject.GetComponent<AspirableObject>().IAmMagnetic)
        {
            if(other.gameObject.GetComponent<AspirableObject>().SpeedToShoot>20)
            {
                ExploteYourChildren();
                
            }
            
        }
    }
    public void ExploteYourChildren()
    {
        Exploted = true;
        foreach (Transform child in this.transform) 
        {
            child.GetComponent<BreakDoorThing>().ActivateExplosion = true;
        }
        boxy.isTrigger = true;
        
    }
}

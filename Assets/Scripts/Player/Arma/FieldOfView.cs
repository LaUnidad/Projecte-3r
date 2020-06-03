using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Touch;
    public GameObject Gun;
    public GameObject Cube;
    

    void Start()
    {
       Gun = GameObject.FindGameObjectWithTag("Gun");
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "AspirableObject")
        {   
            Cube = other.gameObject;
            Gun.GetComponent<Aspiradora>().AddObjects(other.gameObject);
            Touch = true;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "AspirableObject")
        {
            if(!other.gameObject.GetComponent<AspirableObject>().IAmMagnetic)
            {
                other.gameObject.GetComponent<AspirableObject>().rgbd.useGravity = true;
                other.gameObject.transform.localScale = other.gameObject.GetComponent<AspirableObject>().OriginalScale;
            }
            Gun.GetComponent<Aspiradora>().RemoveObjects(other.gameObject);
            Cube = null;
            Touch = false;
        }
    }
    
}

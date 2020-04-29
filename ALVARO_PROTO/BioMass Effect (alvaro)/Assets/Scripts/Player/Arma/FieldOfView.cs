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
            Gun.GetComponent<Aspiradora>().RemoveObjects(other.gameObject);
            Cube = null;
            Touch = false;
        }
    }
    
}

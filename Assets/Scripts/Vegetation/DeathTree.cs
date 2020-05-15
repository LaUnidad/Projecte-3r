using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTree : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject AliveObj;
    GameObject DeathObj;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {    
        //Debug.Log(this.transform.childCount);
        if(this.transform.childCount == 0)
        {
            //Death = true;
            AliveObj.SetActive(false);
            DeathObj.SetActive(true);
        }
        else
        {
            AliveObj.SetActive(true);
            DeathObj.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "LivePlant")
        {
            AliveObj = other.gameObject;
        }
        if(other.tag == "DeathPlant")
        {
            DeathObj = other.gameObject;
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "LivePlant")
        {
            AliveObj = other.gameObject;
        }
        if(other.gameObject.tag == "DeathPlant")
        {
            DeathObj = other.gameObject;
        }
    }  
}

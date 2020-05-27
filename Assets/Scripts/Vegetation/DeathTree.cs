using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTree : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject AliveObj;
    GameObject DeathObj;

    public bool Death;

    public bool rotate;

    public bool KillFruits;

    

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {    
        //Debug.Log(this.transform.childCount);
        TimeToDie();
        if(this.transform.childCount == 0)
        {
            if(!Death && rotate)
            {
                DeathObj.transform.rotation = AliveObj.transform.rotation;
                Death = true;
            }
            AliveObj.GetComponent<DisolveMyChildrens>().Disolve = true;
            //AliveObj.SetActive(false);
            DeathObj.SetActive(true);
            DeathObj.GetComponent<CreateTrial>().Doit = true;
            
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
    public void TimeToDie()
    {
        if(KillFruits)
        {
            foreach (Transform child in this.transform) 
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}

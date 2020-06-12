using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTree : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject AliveObj;
    GameObject DeathObj;

    GameObject Particles;

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
            DeathObj.GetComponent<CreateMyChildren>().Create = true;
            Destroy(Particles.gameObject);
            
        }
        else
        {
            AliveObj.SetActive(true);
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
        if(other.tag == "ParticlesVegetation")
        {
            Particles = other.gameObject;
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
        if(other.gameObject.tag == "ParticlesVegetation")
        {
            Particles = other.gameObject;
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

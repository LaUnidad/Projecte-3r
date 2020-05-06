using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDying : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Eliminate;
     public float MinDistToTreeMaster;
    GameObject[] TreeMaster;
    public bool Die;
    MeshRenderer meshR;

    public float dist;

    public Material DeathMaterial;
    void Start()
    {
        TreeMaster = GameObject.FindGameObjectsWithTag("TreeMaster");
        meshR = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(WichIsMyTreeMaster().GetComponent<DeathTree>().Death);
        
        if(WichIsMyTreeMaster().GetComponent<DeathTree>().Death)
        {
            WelcomToDeath();
        }
    }

    public void WelcomToDeath()
    {
        if(!Eliminate)
        {
            meshR.material = DeathMaterial;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public GameObject WichIsMyTreeMaster()
    {  
        foreach(GameObject obj in TreeMaster)
        {
            dist = Vector3.Distance(obj.transform.position, this.transform.position); 
            if(dist <= MinDistToTreeMaster)
            {
                return obj;
            }
        } 
        return null;  
    }
}

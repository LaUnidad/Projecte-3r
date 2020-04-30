using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColor : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer meshR;
    public Material NormalMat;
    public Material DeathMaterial;
    public bool InContact;
    public float TimeToDie;

    public float timer;

    public bool ImDeath;

    public bool OnList;

    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMaterial();
    }

    void ChangeMaterial()
    {
       
        if(InContact == false)
        {
            timer += 1* Time.deltaTime;
            if(TimeToDie <= timer)
            {   
                ImDeath = true;
                meshR.material = DeathMaterial;
                
            }
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "AspirableObject")
        {
            InContact = true;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "AspirableObject")
        {
            InContact = false;
        } 
    }
    
}

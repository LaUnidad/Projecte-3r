using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanChargingMass : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Gun;

    GameObject Player;

    public bool InVan;


    void Start()
    {
        Gun = GameObject.FindGameObjectWithTag("Gun");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(InVan == true)
        {
            Gun.GetComponent<Aspiradora>().ExpulseBiomass();
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            InVan = true;
        }    
    }
    void OnTriggerExit(Collider other) 
    {
         if(other.tag == "Player")
        {
            InVan = false;
        }   
    }
}

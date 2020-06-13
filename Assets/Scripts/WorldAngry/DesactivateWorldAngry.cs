﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateWorldAngry : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<HippiCharacterController>().AfectedByTheGas = false;
            other.gameObject.GetComponent<HippiCharacterController>().Oasis = true;
        }
    }
}

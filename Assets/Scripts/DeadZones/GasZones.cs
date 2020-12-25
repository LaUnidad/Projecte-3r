using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasZones : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
           //Debug.Log("ENGAS");
            Player.GetComponent<HippiCharacterController>().blackboard.ResistanceToTheGas = 4;
        }
   
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            Player.GetComponent<HippiCharacterController>().blackboard.ResistanceToTheGas = 2;
        }
    }
}

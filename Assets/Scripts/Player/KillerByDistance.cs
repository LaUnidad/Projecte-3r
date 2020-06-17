using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerByDistance : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "DeadController" && player.GetComponent<HippiCharacterController>().blackboard.RoketMan)
        {
            Debug.Log("Contacto");
            other.GetComponent<DeathTree>().KillFruits = true;
        }
    }

}

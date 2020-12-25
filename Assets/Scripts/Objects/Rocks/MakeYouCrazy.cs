using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeYouCrazy : MonoBehaviour
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
        if(other.tag == "FieldOfView" && Player.GetComponent<HippiCharacterController>().Absorving == true)
        {
            Debug.Log("AIRE EN EL OJO!");
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "FieldOfView" && Player.GetComponent<HippiCharacterController>().Absorving == true)
        {
            Debug.Log("AIRE EN EL OJO!");
        }
    }
}

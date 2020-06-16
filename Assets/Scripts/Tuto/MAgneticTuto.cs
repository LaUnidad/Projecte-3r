using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAgneticTuto : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
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
        if(other.tag == "Player")
        {
            if(other.GetComponent<HippiCharacterController>().AfectedByTheGas)
            {
                other.GetComponent<HippiCharacterController>().magneticRock = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToAngryWorld : MonoBehaviour
{
    HippiCharacterController cc;

    // Start is called before the first frame update
    //public GameObject[] TerrainColor;
    GameObject[] MagneticRocks;

    public GameObject[] Craters;
    GameObject Player;

    GameObject ParticleController;
    void Start()
    {
        //TerrainColor = GameObject.FindGameObjectsWithTag("Terrain");
        MagneticRocks = GameObject.FindGameObjectsWithTag("MagneticRock");
        Player = GameObject.FindGameObjectWithTag("Player");
        Craters = GameObject.FindGameObjectsWithTag("Crater");
        ParticleController = GameObject.FindGameObjectWithTag("BadGas");
        ParticleController.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeWorld()
    {
        
        foreach(GameObject obj in Craters)
        {
            obj.gameObject.GetComponent<CraterSize>().Gas = true;
        }
        
        foreach(GameObject obj in MagneticRocks)
        {

            obj.gameObject.tag = "AspirableObject";
        }
        
        Player.GetComponent<HippiCharacterController>().AfectedByTheGas = true;
        ParticleController.gameObject.SetActive(true);
        
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            ChangeWorld();  
        }    
    }
}

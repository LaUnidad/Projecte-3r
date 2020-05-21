using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToAngryWorld : MonoBehaviour
{
    HippiCharacterController cc;

    // Start is called before the first frame update
    public GameObject[] TerrainColor;
    GameObject[] MagneticRocks;
    void Start()
    {
        TerrainColor = GameObject.FindGameObjectsWithTag("Terrain");
        MagneticRocks = GameObject.FindGameObjectsWithTag("MagneticRock");
        cc = FindObjectOfType<HippiCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeWorld()
    {
        foreach(GameObject obj in TerrainColor)
        {
            obj.GetComponent<TerrainMaterial>().Die = true;
        }
        foreach(GameObject obj in MagneticRocks)
        {

            obj.gameObject.tag = "AspirableObject";
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            ChangeWorld();
            cc.isDeadWorldActive = true;
        }    
    }
}

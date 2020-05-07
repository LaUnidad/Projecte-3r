using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacksForTheGame : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode m_KillTheZone = KeyCode.K;
    public GameObject[] TerrainColor;

    GameObject[] MagneticRocks;
    void Start()
    {
        TerrainColor = GameObject.FindGameObjectsWithTag("Terrain");
        MagneticRocks = GameObject.FindGameObjectsWithTag("MagneticRock");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(m_KillTheZone))
        {
            foreach(GameObject obj in TerrainColor)
            {
                obj.GetComponent<TerrainMaterial>().Die = true;
            }
            foreach(GameObject obj in MagneticRocks)
            {

                obj.gameObject.tag = "AspirableObject";
            }
            //AreaKiller.GetComponent<AreaColor>().KillZone = true;
        }
    }
}

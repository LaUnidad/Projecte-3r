using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacksForTheGame : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode m_KillTheZone = KeyCode.K;
    GameObject[] AspirableObj;
    GameObject[] Troncos;
    GameObject AreaKiller;

    GameObject[] MagneticRocks;
    void Start()
    {
        AspirableObj = GameObject.FindGameObjectsWithTag("AspirableObject");
        Troncos = GameObject.FindGameObjectsWithTag("Tronco");
        AreaKiller = GameObject.FindGameObjectWithTag("AreaKiller");
        MagneticRocks = GameObject.FindGameObjectsWithTag("MagneticRock");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(m_KillTheZone))
        {
            foreach(GameObject obj in AspirableObj)
            {
                Destroy(obj);
            }
            foreach(GameObject obj in Troncos)
            {

                obj.GetComponent<TreeColor>().InContact = false;
            }
            foreach(GameObject obj in MagneticRocks)
            {

                obj.gameObject.tag = "AspirableObject";
            }
            AreaKiller.GetComponent<AreaColor>().KillZone = true;
        }
    }
}

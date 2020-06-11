using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacksForTheGame : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode m_KillTheZone = KeyCode.K;

    public KeyCode m_PlayerReciveHit = KeyCode.H;
    public KeyCode m_ReviveBoton = KeyCode.L;

    public KeyCode m_KillingWorld = KeyCode.M;
    public GameObject[] TerrainColor;
    
    public GameObject Player;

    public GameObject InstaniateBio;

    private GameObject[] Doors;

    GameObject[] MagneticRocks;
    private GameObject LeavesBT;
    void Start()
    {
        TerrainColor = GameObject.FindGameObjectsWithTag("Terrain");
        MagneticRocks = GameObject.FindGameObjectsWithTag("MagneticRock");
        Player = GameObject.FindGameObjectWithTag("Player");
        InstaniateBio = GameObject.FindGameObjectWithTag("InstantiateBiomass");
        Doors = GameObject.FindGameObjectsWithTag("BreakableDoor");
        LeavesBT = GameObject.FindGameObjectWithTag("LeavesBigTree");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(m_KillTheZone))
        {
            foreach(GameObject obj in TerrainColor)
            {
                //obj.GetComponent<TerrainMaterial>().Die = true;
            }
            foreach(GameObject obj in MagneticRocks)
            {

                obj.gameObject.tag = "AspirableObject";
            }
            //Player.GetComponent<HippiCharacterController>().isDeadWorldActive = true;
            Player.GetComponent<HippiCharacterController>().AfectedByTheGas = true;
            //AreaKiller.GetComponent<AreaColor>().KillZone = true;
        }
        if(Input.GetKeyDown(m_PlayerReciveHit))
        {
            Player.GetComponent<HippiCharacterController>().PlayerTakeDamage(30);
        }
        if(Input.GetKeyDown(m_KillingWorld))
        {
            foreach(GameObject obj in Doors)
            {
                if(!obj.GetComponent<DoorController>().Exploted)
                {
                    obj.GetComponent<DoorController>().ExploteYourChildren();
                }
            }
            Destroy(LeavesBT.gameObject);

            //ActivaCamaraShake si vols
        }

    }
}

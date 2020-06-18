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

    public bool tutorialCompleted = false;

    private FMOD.Studio.EventInstance Danger;
    private bool done = false;

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

        tutorialCompleted = true;
        
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            ChangeWorld();  
            if (!done)
            {
                Danger = SoundManager.Instance.PlayEvent(GameManager.Instance.Damage, transform);
                done = true;
            }
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Danger.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}

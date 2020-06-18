using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeToSayGoodBye : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Canvas;
    public GameObject MainCAmera;

    public GameObject Van;

    public GameObject Player;

    public GameObject[] fruits;
    public GameObject[] enemys;

    public GameObject AssetsMuertosEnemys;

    
    void Start()
    {
        fruits = GameObject.FindGameObjectsWithTag("DeadController");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        AssetsMuertosEnemys.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            if(other.gameObject.GetComponent<HippiCharacterController>().blackboard.RoketMan)
            {
                AssetsMuertosEnemys.SetActive(true);
                foreach(GameObject obj in fruits)
                {
                    if(!obj.GetComponent<DeathTree>().KillFruits)
                    {
                        obj.GetComponent<DeathTree>().KillFruits= true;
                    }
                }
                foreach(GameObject obj in enemys)
                {
                   obj.gameObject.SetActive(false);
                }
                Destroy(Canvas.gameObject);
                //MainCAmera.SetActive(false);
                Destroy(Player.gameObject);
                this.gameObject.SetActive(false);  
                
            }
        }
    }

    
}

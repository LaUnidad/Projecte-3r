using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeToSayGoodBye : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Canvas;
    public GameObject MainCAmera;

    public GameObject Player;

    public GameObject[] fruits;
    public GameObject[] enemys;

    public GameObject newPos;

    public GameObject Objective;
    public float TimeForGo;

    public float velocity;

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
                
                EncendiendoMotores();
            }
        }
    }

    public void EncendiendoMotores()
    {
        this.transform.position = newPos.transform.position;
        ////__________________________________________ANIMACIÓ ENCENDIENDO MOTORES_______________________________________________________
        Invoke("Despegue", TimeForGo);
    }

    public void Despegue()
    {
        Debug.Log("DESPEQUE");
        ////__________________________________________ANIMACIÓ NAU PIRANT_______________________________________________________
        Vector3.MoveTowards(this.transform.position,Objective.transform.position, 10000);
    }
}

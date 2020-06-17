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
    void Start()
    {
        fruits = GameObject.FindGameObjectsWithTag("DeadController");
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
                Destroy(Canvas.gameObject);
                //MainCAmera.SetActive(false);
                Destroy(Player.gameObject);
                foreach(GameObject obj in fruits)
                {
                    if(!obj.GetComponent<DeathTree>().KillFruits)
                    {
                        obj.GetComponent<DeathTree>().KillFruits= true;
                    }
                }
            }
        }
    }
}

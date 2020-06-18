using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMagneticRock : MonoBehaviour
{
    // Start is called before the first frame update
    public bool InContact;

    public float timer;

    public GameObject SpawnPoint;

    public GameObject m_Rock;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!InContact && player.GetComponent<HippiCharacterController>().AfectedByTheGas)
        {
            /*
            timer += 1*Time.deltaTime;

            if(timer>= 10)
            {
                Instantiate(m_Rock,SpawnPoint.transform.position, m_Rock.transform.rotation);
            }
            */

        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "AspirableObject")
        {
            InContact = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "AspirableObject")
        {
            InContact = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticRockParticles : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ActivateParticles();
    }
    public void ActivateParticles()
    {
        if(player.GetComponent<HippiCharacterController>().AfectedByTheGas)
        {
            foreach(Transform child in this.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(Transform child in this.transform)
            {
                child.gameObject.SetActive(false);
            } 
        }
    }
}

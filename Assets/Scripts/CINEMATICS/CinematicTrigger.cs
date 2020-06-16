using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("LISTA DE CINEMATICAS")]

    public GameObject[] Cinematicas;

    [Header("QUE CINEMATICA QUIERES QUE SE DESARROLLE (CinematicOrder)")]

    public int wichCinematicYouWant;

    [Header("SI ES LA ULTIMA CINEMATICA ACTIVAHO")]
    
    public bool FinalCinematic;

    private GameObject player;


    void Start()
    {
        Cinematicas = GameObject.FindGameObjectsWithTag("Cinematic");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DesactivateCondition();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && !FinalCinematic)
        {
            ActivateTheCorrectCinematic();
        }
    }

    public void ActivateTheCorrectCinematic()
    {
        foreach(GameObject obj in Cinematicas)
        {
            if(obj.GetComponent<Cinamatic1>().CinematicOrder == wichCinematicYouWant)
            {
                obj.GetComponent<Cinamatic1>().ImActive = true;
                obj.gameObject.SetActive(true);
            }
        }
    }

    public void DesactivateCondition()
    {
        if(FinalCinematic && player.GetComponent<HippiCharacterController>().blackboard.RoketMan)
        {
            FinalCinematic = false;
        }
    }

    

    
}

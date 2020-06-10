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

    


    void Start()
    {
        Cinematicas = GameObject.FindGameObjectsWithTag("Cinematic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
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

    

    
}

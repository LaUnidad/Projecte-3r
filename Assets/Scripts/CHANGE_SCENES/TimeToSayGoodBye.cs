using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeToSayGoodBye : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }
}

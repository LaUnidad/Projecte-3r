using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    CharacterController cc;
    public GameObject teleport1;
    public GameObject teleport2;

    private bool goingTo2;
    private bool goingTo1;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent < CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Teleport1")
        {
            goingTo2 = true;

            if (goingTo2)
            {
                Debug.Log("Trigger Teleport 1");
                cc.enabled = false;
                transform.position = teleport2.transform.position;
                cc.enabled = true;
            }
        }
        if (other.tag == "Teleport2")
        {
            if(!goingTo2)
            {
                cc.enabled = false;
                transform.position = teleport1.transform.position;
                cc.enabled = true;
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Teleport1")
        {

        }
        if (other.tag == "Teleport2") 
        {
            goingTo2 = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{

    public GameObject destroyedObject;


    void OnMouseDown()
    {
        Instantiate(destroyedObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(destroyedObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

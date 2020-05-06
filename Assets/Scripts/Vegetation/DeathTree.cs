using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTree : MonoBehaviour
{
    // Start is called before the first frame update
       public static List<GameObject> Fruits = new List<GameObject>();
    public bool Death;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Fruits.Count == 0)
        {
            Death = true;
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "AspirableObject")
        {
            AddObjectToList(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "AspirableObject" || other.gameObject == null)
        {
            RemoveObjectToList(other.gameObject);
        }
    }
    void AddObjectToList(GameObject x)
    {
        Fruits.Add(x);
    }
    void RemoveObjectToList(GameObject x)
    {
        Fruits.Remove(x);
    }
}

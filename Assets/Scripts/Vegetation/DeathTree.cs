using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTree : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<GameObject> Fruits = new List<GameObject>();
    public bool Death;
    GameObject AliveObj;
    GameObject DeathObj;

    public bool IHaveFruits;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Death)
        {
            
        }
        
        if(!IHaveFruits)
        {
            //Death = true;
            AliveObj.SetActive(false);
            DeathObj.SetActive(true);
        }
        else
        {
            AliveObj.SetActive(true);
            DeathObj.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "AspirableObject")
        {
            //AddObjectToList(other.gameObject);
            IHaveFruits = true;
        }
        if(other.tag == "LivePlant")
        {
            AliveObj = other.gameObject;
        }
        if(other.tag == "DeathPlant")
        {
            DeathObj = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "AspirableObject" || other.gameObject == null)
        {
            IHaveFruits = false;
            //RemoveObjectToList(other.gameObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoors : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] doors;
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("BreakableDoor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyAllDoors()
    {
        foreach(GameObject obj in doors)
        {
            if(!obj.GetComponent<DoorController>().Exploted)
            {
                obj.GetComponent<DoorController>().ExploteYourChildren();
            }
        }
    }
}

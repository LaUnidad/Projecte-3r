using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColor : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Area1Obj;
    
    public float NumOfDeads;
    
    public bool KillZone;

    void Start()
    {
        Area1Obj = GameObject.FindGameObjectsWithTag("Tronco");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
   

}

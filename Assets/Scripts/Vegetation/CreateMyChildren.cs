using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMyChildren : MonoBehaviour
{
    
    public bool Create;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Create)
        {
            for(int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.GetComponent<CreateTrial>().Doit = true;
            }
        }
        
    }
    
}

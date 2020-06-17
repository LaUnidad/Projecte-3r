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
            if(this.transform.childCount != 0)
            {
                for(int i = 0; i < this.transform.childCount; i++)
                {
                    if( this.transform.GetChild(i).gameObject != null)
                    {
                        this.transform.GetChild(i).gameObject.GetComponent<CreateTrial>().Doit = true;
                    }
                    else
                    {
                        Debug.Log("NO MAS ERRORES");
                    }
                    
                }
            }
            else
            {
                Debug.Log("NO MAS ERRORES");
            }
            
        }
        
    }
    
}

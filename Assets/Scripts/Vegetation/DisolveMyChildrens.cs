using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveMyChildrens : MonoBehaviour
{
    // Start is called before the first frame update

    public bool Disolve;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Disolve)
        {
            for(int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.GetComponent<DisolveTrial>().Doit = true;
            }
        }
        
    }
}

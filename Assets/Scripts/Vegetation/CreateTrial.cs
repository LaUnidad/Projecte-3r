using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTrial : MonoBehaviour
{
    private MeshRenderer mesR;

    public bool Doit;

    public float timer;

    [Range (0,10)]public float VelocityToDisapear;
    void Start()
    {
        
        mesR = GetComponent<MeshRenderer>();
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        mesR.material.SetFloat("VelocityToApear", timer);
        if(Doit)
        {
           
            timer -= 1* Time.deltaTime * (VelocityToDisapear/10);    
        }
        

    }
}

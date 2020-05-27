using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTrial : MonoBehaviour
{
    MeshRenderer mesR;

    public Material disolveMat;
    public bool Doit;

    public float timer;

    [Range (0,10)]public float VelocityToDisapear;
    void Start()
    {
        mesR = GetComponent<MeshRenderer>();
        timer = 1;
    }
    void Update()
    {
        disolveMat.SetFloat("Vector1_1D89C180", timer);
        
        if(Doit)
        {
            timer -= 1* Time.deltaTime * (VelocityToDisapear/10);    
        }
        

    }
}

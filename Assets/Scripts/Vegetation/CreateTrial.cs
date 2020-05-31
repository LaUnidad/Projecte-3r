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
        disolveMat.SetFloat("Vector1_226A2816", 1);
        mesR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        disolveMat.SetFloat("Vector1_226A2816", timer);
        Debug.Log(timer);
        if(Doit)
        {
           
            timer += 1* Time.deltaTime * (VelocityToDisapear/10);    
        }
        

    }
}

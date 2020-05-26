using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisolveTrial : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesR;

    public Material disolveMat;

    public bool Disolve;
    public bool Doit;

    public float timer;

    [Range (0,10)]public float VelocityToDisapear;
    void Start()
    {
        WichIsYourState();
        mesR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        disolveMat.SetFloat("Vector1_226A2816", timer);
        if(Doit && Disolve)
        {
            timer += 1* Time.deltaTime * (VelocityToDisapear/10);  
            if(timer>= 1)
            {
                Destroy(this.gameObject);
            }  
        }
        else if(Doit && !Disolve)
        {
            timer -= 1* Time.deltaTime * (VelocityToDisapear/10);  
        }
        
        

    }
    public void WichIsYourState()
    {
        if(Disolve)
        {
            disolveMat.SetFloat("Vector1_226A2816", 0);
        }
        else
        {
            disolveMat.SetFloat("Vector1_226A2816", 1);
        }
    }
}

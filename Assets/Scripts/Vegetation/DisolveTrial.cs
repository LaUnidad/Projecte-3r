using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisolveTrial : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer mesR;
    public bool Doit;

    public float timer;

    [Range (0,10)]public float VelocityToDisapear;
    void Start()
    {
       
        mesR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mesR.material.SetFloat("VelocityToDisapear", timer);

        if(Doit)
        {
            timer += 1* Time.deltaTime * (VelocityToDisapear/10);  
            if(timer>= 1)
            {
                Destroy(this.gameObject);
            }  
        }
       
        
        

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveMyChildrens : MonoBehaviour
{
    // Start is called before the first frame update

    public bool Disolve;

    
    [Header("ACTIVAR EN CASO DE TENER PARTÍCULAS PARA LA MUERTE:")]
    public bool activateParticlesDeath;

    [Header("ARRASTRAR PARTÍCULAS:")]
    public GameObject particles;
    
    void Start()
    {
        if(activateParticlesDeath)
        {
            particles.SetActive(false); 
        }
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
            if(activateParticlesDeath && particles != null)
            {
                particles.SetActive(true);            
            }
            else
            {
                Debug.Log("CHECK OUT THE VARIABLE or YOU PROBABLY DONT ASSIGN THE PARTICLES. KEEP WORKING BRO.");
            }
            
        }
        
    }
}

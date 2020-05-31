using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterSize : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ParticleSystem;
    public bool IAmMagneticCrater;

    public bool Gas;
    void Start()
    {
        ParticleSystem.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        if(Gas && !IAmMagneticCrater)
        {
            //this.transform.localScale = new Vector3(this.transform.localScale.x,this.transform.localScale.y,150);
            ParticleSystem.SetActive(true);
        }
        
    }
}

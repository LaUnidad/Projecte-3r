using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1*Time.deltaTime;
        if(timer>= 3)
        {
            Destroy(this.gameObject);   
        }
    }
}

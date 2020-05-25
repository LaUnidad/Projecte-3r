using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer meshR;
    public Material Death;
    
    public bool Die;

    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Die == true)
        {
            meshR.material = Death;
        }   
    }
}

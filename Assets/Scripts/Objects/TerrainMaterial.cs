using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer meshR;
    public Material Death;

     GameObject AreaKiller;
    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        AreaKiller = GameObject.FindGameObjectWithTag("AreaKiller");
    }

    // Update is called once per frame
    void Update()
    {
        if(AreaKiller.GetComponent<AreaColor>().KillZone == true)
        {
            meshR.material = Death;
        }
    }
}

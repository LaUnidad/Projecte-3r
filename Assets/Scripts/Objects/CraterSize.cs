using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (TerrainMaterial))]
public class CraterSize : MonoBehaviour
{
    // Start is called before the first frame update
    TerrainMaterial m_TM;
    public GameObject ParticleSystem;
    void Start()
    {
        ParticleSystem.SetActive(false);
        m_TM = GetComponent<TerrainMaterial>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_TM.Die)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x,this.transform.localScale.y,1);
            ParticleSystem.SetActive(true);
        }
    }
}

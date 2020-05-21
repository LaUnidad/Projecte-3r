using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (TerrainMaterial))]
public class CraterSize : MonoBehaviour
{
    // Start is called before the first frame update
    TerrainMaterial m_TM;
    public GameObject ParticleSystem;
    public bool IAmMagneticCrater;
    void Start()
    {
        ParticleSystem.SetActive(false);
        m_TM = GetComponent<TerrainMaterial>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_TM.Die && !IAmMagneticCrater)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x,this.transform.localScale.y,150);
            ParticleSystem.SetActive(true);
        }
        else if(m_TM.Die && IAmMagneticCrater)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x,this.transform.localScale.y,100);
        }
    }
}

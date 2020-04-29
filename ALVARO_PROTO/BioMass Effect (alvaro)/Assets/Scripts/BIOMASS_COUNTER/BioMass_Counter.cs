using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioMass_Counter : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 InitialScale;
    

    GameObject gun;

    public float NewBio;

    public float DesChargeVelocity;
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun");
        InitialScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        InitialSize();
        UpdatePos();
        //ChargeBioMass();
    }
    public void UpdatePos()
    {
        float actualBio;
        actualBio = gun.GetComponent<Aspiradora>().Biomass;
        //Debug.Log("ACTUALBIO-->"+actualBio +"NEWBIO-->"+NewBio);
        
        if(actualBio > NewBio)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x +(actualBio/10000),this.transform.localScale.y +(actualBio/10000),this.transform.localScale.z +(actualBio/10000));
            NewBio = actualBio;         
        }
        if(actualBio < NewBio)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x -(actualBio/1000)*DesChargeVelocity*Time.deltaTime,this.transform.localScale.y -(actualBio/1000)*DesChargeVelocity*Time.deltaTime,this.transform.localScale.z -(actualBio/1000)*DesChargeVelocity*Time.deltaTime);
            NewBio = actualBio;         
        }
    }
    public void InitialSize()
    {
        if(this.transform.localScale.x <= InitialScale.x)
        {
            this.transform.localScale = InitialScale;
        }

        if(gun.GetComponent<Aspiradora>().Biomass == 0)
        {
            this.transform.localScale = InitialScale;
        }
    }
    
}

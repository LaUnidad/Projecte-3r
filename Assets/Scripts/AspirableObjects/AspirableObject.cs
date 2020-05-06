using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AspirableObject : MonoBehaviour
{

    public bool IAmMagnetic;
    public bool IAmAsborved;
    public bool IAmInList;
    public float ForceToAbsorb;
    public float SpeedToAbsorb;
    public float SpeedToShoot;

    Rigidbody rgbd;
    private GameObject Player;

    private GameObject Gun;

    public float Biomass;

    public float distance;

    Vector3 OriginalScale;

    public bool ImAbsorved;

    public bool Enganchao;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gun = GameObject.FindGameObjectWithTag("Gun");
        rgbd = GetComponent<Rigidbody>();
        OriginalScale = this.transform.localScale;

    }
    void Update()
    {   
        if(IAmInList == true)
        {
           Absorbing();
        }  
        Shooting();  
    }
    public void Shooting()
    {
        if(Enganchao == true)
        {
            this.transform.position = Gun.transform.position;
              
            if(Player.GetComponent<HippiCharacterController>().Shootting)
            {
                //Debug.Log("BUUUUUUM");
                //rgbd.useGravity = true;
                this.transform.SetParent(null);
                this.transform.position = transform.position + Player.transform.forward;
                rgbd.velocity = Player.transform.forward * SpeedToShoot;
            }   
        }
    }
    public void Absorbing()
    {
        distance = Vector3.Distance(this.transform.position, Gun.transform.position);

        if(Player.GetComponent<HippiCharacterController>().Absorving == true)
        {
            //Debug.Log("ABSORVIENDO");
            ImAbsorved = true;
            rgbd.useGravity = false;
            rgbd.isKinematic = false;
            this.transform.position = Vector3.MoveTowards(transform.position, Gun.transform.position, SpeedToAbsorb*Time.deltaTime);
                
            if(!IAmMagnetic)
            {
                this.transform.localScale = new Vector3(transform.localScale.x * 0.99f,transform.localScale.y * 0.99f,transform.localScale.z * 0.99f);
            }
        }
        else
        {
            if(!IAmMagnetic)
            {
                
                rgbd.useGravity = true;
                this.transform.localScale = OriginalScale;
                ImAbsorved = false;
            }
            else
            {
                rgbd.useGravity = false;
                 ImAbsorved = false;
            }
           
            
           
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Gun" && IAmMagnetic)
        {
            //Debug.Log("IEP");
            
            rgbd.useGravity = false;
            Player.GetComponent<HippiCharacterController>().ICanAbsorbThis = true;
            Enganchao = true;
        }
    }
    void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Gun")
        {
            Player.GetComponent<HippiCharacterController>().ICanAbsorbThis = false;
            Enganchao = false;
        }
    }
    
   
}

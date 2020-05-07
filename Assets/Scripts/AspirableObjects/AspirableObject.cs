using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AspirableObject : MonoBehaviour
{
    public bool IAmMagnetic;

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

    public bool Shooot;

    public Vector3 direction;

    public GameObject MyTarget;

    public float TimeToReturn;

    float timer;

    bool Shooted;


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
        
        if(IAmMagnetic)
        {
            //Debug.Log("OnSide->"+ImOnSide()+"  Absorving->"+ImAbsorved+"  Shooted->"+Shooted);
            Shooting();
            BeingShooted(); 
            MagneticRockState(); 
        }
        
    }
    public void Shooting()
    {      
        if(Shooot)
        {     
            this.transform.position = transform.position + direction * Time.deltaTime;
            rgbd.velocity = direction * SpeedToShoot;
            Shooted = true;
        }     
    }

    public void BeingShooted()
    {
        if(Shooted)
        {
            timer += 1* Time.deltaTime;
            

            if(timer >= TimeToReturn)
            { 
                
                Shooot = false;
                Shooted = false;
               
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
            ImAbsorved = false; 
            if(!IAmMagnetic)
            {
                rgbd.useGravity = true;
                this.transform.localScale = OriginalScale;    
            }
        }
    }
    public void MagneticRockState()
    {
        if(IAmMagnetic && this.gameObject.tag == "AspirableObject")
        {
            if(ImOnSide()==false && ImAbsorved == false && Shooted == false)
            {
                Debug.Log("TOT FALÇ");
                timer = 0;
                Shooot = false;
                direction = new Vector3(0,0,0);
                transform.LookAt(MyTarget.transform.position, transform.position + transform.forward);
                this.transform.position = transform.position + transform.forward * 0.1f;
                rgbd.velocity = Player.transform.forward * SpeedToShoot;
                //Vector3.MoveTowards(this.transform.position, target.transform.position, 10* Time.deltaTime);
                
            }
            else if(ImOnSide()== true && ImAbsorved == false && Shooted == false)
            {
                this.transform.position = MyTarget.transform.position;
            }
        }
    }

    public bool ImOnSide()
    {
        float dist;
        dist = Vector3.Distance(MyTarget.transform.position, this.transform.position);
        //Debug.Log(dist);
        if(dist >= 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Gun" && IAmMagnetic)
        {
            //Debug.Log("IEP");
            
            rgbd.useGravity = false;
            Player.GetComponent<HippiCharacterController>().ICanAbsorbThis = true;
            
        }
    }
    void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Gun")
        {
            Player.GetComponent<HippiCharacterController>().ICanAbsorbThis = false;
            
        }
    }
    
   
}

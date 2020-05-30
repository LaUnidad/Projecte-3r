using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (SphereCollider))]
public class AspirableObject : MonoBehaviour
{
    public bool IMakeDamage;
    public bool IAmMagnetic;
    public bool IAmAsborved;
    public bool IAmInList;
    public float ForceToAbsorb;
    public float SpeedToAbsorb;
    public float SpeedToShoot;

    public float MinDistToGeiser;

    public Rigidbody rgbd;
    private GameObject Player;

    private GameObject Gun;

    public float Biomass;

    public float LifeForThePlayer;

    public float distance;

    public Vector3 OriginalScale;

    public bool ImAbsorved;

    public GameObject target;

    public float timer;
    public bool ImShooted;

    public Vector3 PlayerForward;

    bool TouchingCrater;

    bool DoIt1Time;

    public bool BeenAbsorved;

    private SphereCollider coll;
    
    

    

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gun = GameObject.FindGameObjectWithTag("Gun");
        coll = GetComponent<SphereCollider>();
        rgbd = GetComponent<Rigidbody>();
        OriginalScale = this.transform.localScale;
        

    }
    void Update()
    {   
        if(IAmInList == true)
        {
           Absorbing();
        }  
        if(IAmMagnetic && this.gameObject.tag == "AspirableObject")
        {   
            VelocityChanger(timer);
            Debug.Log(StopAbsorvingMagneticRock());  
            if(StopAbsorvingMagneticRock())
            {
                ReturnHome();  
            } 
        } 
        ///////////////////////////////////////////////////TIME TO GO//////////////////////////////////////////////////////
        if(!IAmMagnetic)
        {
            DieWithTime(8);
        }
            
    }
    public void StopBeingShooted()
    {
        Player.GetComponent<HippiCharacterController>().Shootting = false;
        ImShooted = false;
        ReturnHome();
    }
    public void ReturnHome()
    {
        Debug.Log("Take Me Home");
        BeenAbsorved = false;
        //coll.isTrigger = true;
        transform.LookAt(target.transform.position, transform.position + transform.forward);
        this.transform.position = transform.position + transform.forward * 0.1f;
        rgbd.velocity = transform.forward * SpeedToShoot;
    }
    public void Shooting()
    {      
        ImAbsorved = false;
        this.transform.position = transform.position + PlayerForward;
        rgbd.velocity = PlayerForward * SpeedToShoot;
        ImShooted = true;
        Invoke("StopBeingShooted", 3);    
    }
    public void Absorbing()
    {
        distance = Vector3.Distance(this.transform.position, Gun.transform.position);

        if(Player.GetComponent<HippiCharacterController>().Absorving == true)
        {
            //Debug.Log("ABSORVIENDO");
            this.transform.LookAt(Gun.transform.position);
            ImAbsorved = true;
            IMakeDamage = false;
            rgbd.useGravity = false;
            rgbd.isKinematic = false;
            this.transform.position = Vector3.MoveTowards(transform.position, Gun.transform.position, SpeedToAbsorb*Time.deltaTime);
            BeenAbsorved = true;
            timer += 1* Time.deltaTime;
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
                //ReturnHome();
                rgbd.useGravity = false;
                ImAbsorved = false;    
            } 
        }
    }
    public void Pos2()
    {
        if(this.gameObject.tag == "AspirableObject" && DoIt1Time)
        {
            this.transform.position = target.transform.position;
            DoIt1Time = false;
        }
    }
    public float DistanceToTarget()
    {
       float dist = Vector3.Distance(this.transform.position, target.transform.position);
       return dist;
    }
    public bool IsOnSide()
    {
        if(!TouchingCrater)
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
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Crater")
        {
            TouchingCrater = true;

            if(!ImAbsorved && !ImShooted && this.gameObject.tag == "AspirableObject")
            {
                this.transform.position = target.transform.position;
                rgbd.isKinematic = true;
                timer = 0;
                coll.isTrigger = false;
            }
        }
        if(other.tag == "PlayerCollider" && IMakeDamage)
        {
            Player.GetComponent<HippiCharacterController>().PlayerReciveDamage(5);
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Crater")
        {
            TouchingCrater = false;
        }
    }
    void DieWithTime(float x)
    {
        if(BeenAbsorved)
        {
            Invoke("MyTimeHasArrive", x);

        }    
    }
    void MyTimeHasArrive()
    {
        Destroy(this.gameObject);
        //Player.GetComponent<HippiCharacterController>().currentHealth += 1;
    }

    public bool StopAbsorvingMagneticRock()
    {
        if(BeenAbsorved && !Player.GetComponent<HippiCharacterController>().Absorving && !ImShooted && !TouchingCrater)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void VelocityChanger (float x)
    {
        if(x<= 1)
        {
            SpeedToShoot = 8;
        }
        else if(x> 1 && x<= 2.5)
        {
            SpeedToShoot = 1 * (x*10);
        }
        else if(x>2.5)
        {
            SpeedToShoot = 25;
        }
    }
    
}

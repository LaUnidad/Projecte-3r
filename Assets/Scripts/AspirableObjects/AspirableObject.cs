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

    public float MinDistToGeiser;

    Rigidbody rgbd;
    private GameObject Player;

    private GameObject Gun;

    public float Biomass;

    public float distance;

    Vector3 OriginalScale;

    public bool ImAbsorved;

    public bool Enganchao;

    GameObject target;

    public float TimeToReturn;

    public float timer;
    public bool ImShooted;

    public Vector3 PlayerForward;

    

    

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gun = GameObject.FindGameObjectWithTag("Gun");
        FindYourTarget();
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
        if(IAmMagnetic)
        {   
            Debug.Log("ABSORVING"+ImAbsorved+" ON SIDE:"+IsOnSide()+" SHOOTED:"+ImShooted);
        }
        MagneticRockState();
        TimeToBeShoot();
        
    }
    public void TimeToBeShoot()
    {
        if(ImShooted)
        {
            timer += 1 * Time.deltaTime;
            
            if(timer>=TimeToReturn)
            {
                ImShooted = false;
                timer = 0;
                Player.GetComponent<HippiCharacterController>().Shootting = false;
            }
            
        }
    }
    public void Shooting()
    {
              
        if(Player.GetComponent<HippiCharacterController>().Shootting)
        {
            //Debug.Log("BUUUUUUM");
            //rgbd.useGravity = true;
            //this.transform.SetParent(null);
            ImAbsorved = false;
            this.transform.position = transform.position + PlayerForward * 0.1f;
            rgbd.velocity = Player.transform.forward * SpeedToShoot;
            ImShooted = true;
                

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
    public void MagneticRockState()
    {
        if(IAmMagnetic)
        {
            Debug.Log("EI");
            if(IsOnSide()==false && ImAbsorved == false && ImShooted == false)
            {
                Debug.Log("QUE PASA LOKOOO");
                transform.LookAt(target.transform.position, transform.position + transform.forward);
                this.transform.position = transform.position + transform.forward * 0.1f;
                rgbd.velocity = Player.transform.forward * SpeedToShoot;
                //Vector3.MoveTowards(this.transform.position, target.transform.position, 10* Time.deltaTime);
            }
            else if(IsOnSide()== true && ImAbsorved == false && ImShooted == false)
            {
                transform.LookAt(target.transform.position, transform.position + transform.forward);
                this.transform.position = transform.position + transform.forward * 0.1f;
                rgbd.velocity = Player.transform.forward * SpeedToShoot;
            }
        }
    }
    public float DistanceToTarget()
    {
       float dist = Vector3.Distance(this.transform.position, target.transform.position);
       return dist;
    }
    public bool IsOnSide()
    {
        if(DistanceToTarget()>= MinDistToGeiser)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void FindYourTarget()
    {
        if(IAmMagnetic)
        {
            target = GameObject.FindGameObjectWithTag("MagneticTarget");
        }
        else
        {
            target = null;
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
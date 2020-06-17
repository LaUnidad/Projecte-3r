using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (SphereCollider))]
public class AspirableObject : MonoBehaviour
{
    [Header("TIPO DE ASPIRABLEOBJECT")]
    public bool THEBIGONE;
    public bool IMakeDamage;
    public bool IAmMagnetic;
    public bool IAmAsborved;
    public bool IAmInList;

    public bool Heart;

    public bool ImLive;

    [Range(1,3)]
    public int Type;

    [Header("VARIABLES DEL ASPIRABLEOBJECT")]
    public float ForceToAbsorb;
    public float SpeedToAbsorb;
    public float SpeedToShoot;
    public float MinDistToGeiser;

    public float Biomass;

    public float LifeForThePlayer;

    [Header("VARIABLES AUTOMATICAS, NI CASO")]
    public Rigidbody rgbd;

    public Vector3 OriginalScale;
    public Vector3 PlayerForward;
    private GameObject Player;

    private GameObject Gun;
    private SphereCollider coll;

    
    [Header("SI LA ROCA ES MAGNETICA NECESITA EL TARGET")]
    public GameObject target;
    float distance;
    bool ImAbsorved;

    float timer;
    bool ImShooted;

    bool TouchingCrater;

    bool DoIt1Time;

    bool BeenAbsorved;
    
    float TimeToReturn;
    
    float XPos;

    float YPos;

    float ZPos;
    Vector3  rotationPoint;

    float RandomSpeed;

    public float StartDistance;

    public bool booleanForTheScale;

    Vector3 TheOtherOriginalScale;

    public float timer2;



    

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gun = GameObject.FindGameObjectWithTag("Gun");
        coll = GetComponent<SphereCollider>();
        rgbd = GetComponent<Rigidbody>();
        OriginalScale = this.transform.localScale;
        RandomSpeed = Random.Range(0.7f,1);
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
            //Debug.Log(StopAbsorvingMagneticRock());  
            if(StopAbsorvingMagneticRock())
            {
                //ReturnHome();
                Shooting();  
            } 
        } 
        ///////////////////////////////////////////////////TIME TO GO//////////////////////////////////////////////////////
        if(!IAmMagnetic && !THEBIGONE)
        {
            DieWithTime(8);
        }
        

        if(ImLive)
        {
            rgbd.useGravity = true;
            rgbd.isKinematic = false;
            Invoke("KillLiveAsset", 8);
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
        coll.isTrigger = true;
        transform.LookAt(target.transform.position, transform.position + transform.forward);
        this.transform.position = transform.position + transform.forward * 0.1f;
        rgbd.velocity = transform.forward * SpeedToShoot;
    }
    public void Shooting()
    {      
        ImAbsorved = false;
        PlayerForward = Player.gameObject.transform.forward;
        this.transform.position = transform.position + PlayerForward;
        rgbd.velocity = PlayerForward * SpeedToShoot;
        ImShooted = true;
        Invoke("StopBeingShooted", TimeToReturn);    
    }
    public void Absorbing()
    {
        

        if(Player.GetComponent<HippiCharacterController>().Absorving == true)
        {

            //Debug.Log("ABSORVIENDO");
            //this.transform.LookAt(Gun.transform.position);
            //ImAbsorved = true;
            //IMakeDamage = false;
            //rgbd.useGravity = false;
            //rgbd.isKinematic = false;
            //this.transform.position = Vector3.MoveTowards(transform.position, Gun.transform.position, SpeedToAbsorb*Time.deltaTime);
            //BeenAbsorved = true;
            //timer += 1* Time.deltaTime;
            if(!IAmMagnetic && !THEBIGONE)
            {
                //this.transform.localScale = new Vector3(transform.localScale.x * 0.99f,transform.localScale.y * 0.99f,transform.localScale.z * 0.99f);
                 AbsorbInSpiral();
            }
            else if(THEBIGONE)
            {
                //AbsorbInSpiral();
                AbsorbingMagnetic();
            }
            else if(IAmMagnetic)
            {
                AbsorbingMagnetic();
            }
        }
        else
        {
            if(!IAmMagnetic )
            {
                rgbd.isKinematic = !BeenAbsorved;
                booleanForTheScale = false;
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
            //Player.GetComponent<HippiCharacterController>().ICanAbsorbThis = true;
            
        }
        
    }  
    void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Gun")
        {
            //Player.GetComponent<HippiCharacterController>().ICanAbsorbThis = false;
            
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "CraterCollider")
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
            Player.GetComponent<HippiCharacterController>().PlayerTakeDamage(5);
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "CraterCollider")
        {
            TouchingCrater = false;
            
        }
    }
    void DieWithTime(float x)
    {
        if(BeenAbsorved)
        {
            //this.transform.parent = null;
            switch (Type)
            {
                case 1:
                    //SoundManager.Instance.PlayOneShotSound(GameManager.Instance.AbsorbableSmall, transform, true, 0.2f);
                    break;
                case 2:
                    //SoundManager.Instance.PlayOneShotSound(GameManager.Instance.AbsorbableNormal, transform, true, 0.2f);
                    break;
                case 3:
                    //SoundManager.Instance.PlayOneShotSound(GameManager.Instance.AbsorbableBig, transform, true, 0.2f);
                    break;
                default:
                    //SoundManager.Instance.PlayOneShotSound(GameManager.Instance.AbsorbableBig, transform, true, 0.2f);
                    break;
            }
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
            TimeToReturn = 1;
        }
        else if(x> 1 && x<= 2.5)
        {
            SpeedToShoot = 1 * (x*10);
            TimeToReturn = 2;
        }
        else if(x>2.5)
        {
            SpeedToShoot = 25;
            TimeToReturn = 3;
        }
    }

    public void AbsorbingMagnetic()
    {
        this.transform.LookAt(Gun.transform.position);
        ImAbsorved = true;
        IMakeDamage = false;
        rgbd.useGravity = false;
        rgbd.isKinematic = false;
        this.transform.position = Vector3.MoveTowards(transform.position, Gun.transform.position, SpeedToAbsorb*Time.deltaTime);
        BeenAbsorved = true;
        timer += 1* Time.deltaTime;
    }
    public void AbsorbInSpiral()
    {
        if(!booleanForTheScale)
        {
            StartDistance = Vector3.Distance(this.transform.position, Gun.transform.position);
            TheOtherOriginalScale = new Vector3(OriginalScale.x + 0.3f, OriginalScale.y + 0.3f, OriginalScale.z +0.3f);
            
        }
        distance = Vector3.Distance(this.transform.position, Gun.transform.position);
        rotationPoint = Gun.transform.position + (Gun.transform.forward * distance);
        this.transform.RotateAround(rotationPoint, Gun.transform.forward, Time.deltaTime*720*RandomSpeed);
        ImAbsorved = true;
        IMakeDamage = false;
        rgbd.useGravity = false;
        rgbd.isKinematic = false;
        this.transform.position = Vector3.MoveTowards(transform.position, Gun.transform.position, SpeedToAbsorb*Time.deltaTime*RandomSpeed);
        BeenAbsorved = true;
        booleanForTheScale = true;
        timer += 1* Time.deltaTime;
        float escala = distance/StartDistance;
        //Debug.Log("SD"+ StartDistance + "  D" + distance + " E" + escala);
        //this.transform.localScale = new Vector3(OriginalScale.x * escala,OriginalScale.y * escala,OriginalScale.z * escala);
        this.transform.localScale = new Vector3(TheOtherOriginalScale.x * escala,TheOtherOriginalScale.y * escala,TheOtherOriginalScale.z * escala);  
    }

    public void KillLiveAsset()
    {
        Destroy(this.gameObject);
    }
    
}

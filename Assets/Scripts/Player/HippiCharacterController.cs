using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (BLACKBOARD_ThirdPersonCharacter))]
[RequireComponent(typeof (CharacterController))]
public class HippiCharacterController : MonoBehaviour, IRestartGameElement
{
    GameManager gameManager;

    // ----- RESTART -----
    Vector3 restartPosition;
    Quaternion restartRotation;

    public Animator anim;
    public BLACKBOARD_ThirdPersonCharacter blackboard;
    private CharacterController m_CharacterController;

    public bool UsingGadget;

    public bool NoPower;

    public bool AfectedByTheGas;

    public bool StopLook;

    public bool Absorving;

    public bool ICanAbsorbThis;

    public bool Shootting;


    public GameObject vaccumCone;


    //HEALTH
    public bool Damage;

    public float maxHealth;

    public float TimeAtFalling;

    //[HideInInspector]
    public float currentHealth;

    bool playerDead;

    public bool isDeadWorldActive = false;

    void Awake()
    {
        maxHealth = 100;
        //currentHealth = maxHealth;

        restartPosition = transform.position;
        restartRotation = transform.rotation;
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddRestartGameElement(this);

        m_CharacterController = GetComponent<CharacterController>();
        blackboard = GetComponent<BLACKBOARD_ThirdPersonCharacter>();
        Cursor.visible = false;

        vaccumCone.SetActive(false);

        playerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////////////////ABSORB/////////////////////////////////////////////////////////////
        if ((Input.GetMouseButton(blackboard.m_Absorb) || Input.GetButton("Right Trigger")) && ICanAbsorbThis == false && !NoPower)
        {
            UsingGadget = true;
            
            if(blackboard.Power >= 0)
            {
                if(blackboard.Gun.GetComponent<Aspiradora>().ListObjects != 0)
                {
                    Absorving = true;
                    blackboard.ForceAtAbsorb = blackboard.Gun.GetComponent<Aspiradora>().ForceToThePlayer();
                    StopLook = true;

                  //  anim.SetBool("Absorbing", true);                  
                }
                else
                {
                    StopLook = false;
                }      
            }  
            else
            {
               Absorving = false;
               // anim.SetBool("Absorbing", false);
            }

            //if (Absorving) anim.SetBool("Absorbing", true);
           // if (!Absorving) anim.SetBool("Absorbing", false);
        }
        else
        {
            UsingGadget = false;
            Absorving = false;
            blackboard.ForceAtAbsorb = 1;
            StopLook = false;
            //Debug.Log("EO");
        }

        if (UsingGadget)
        {
            vaccumCone.SetActive(true);
            anim.SetBool("Absorbing", true);
        }
        else
        {
            vaccumCone.SetActive(false);
            anim.SetBool("Absorbing", false);
        }
        ////////////////////////////////////////////////LIFE//////////////////////////////////////////////////////
        RestLife();
        
        /*
        if (isDeadWorldActive)
        {
            ReducePlayerHealth();
        }
        */
        if (blackboard.currentLife <= 0)
        {
            m_CharacterController.enabled = false;
            gameManager.RestartGame();
        }
        
       // Debug.Log("Current health:  " + currentHealth);
       //////////////////////////////////////////////POWER///////////////////////////////////////////////////////
        UsePower();
       /////////////////////////////////////////////////DAMAGE///////////////////////////////////////////////////
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            restartPosition = other.transform.position;
            restartRotation = other.transform.rotation;
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restart Player");
        Time.timeScale = 1f;
        this.transform.position = restartPosition;
        this.transform.rotation = restartRotation;
        blackboard.currentLife = blackboard.MaxLife;
        m_CharacterController.enabled = true;
        playerDead = false;
        
    }
   
    void UsePower()
    {
        if(UsingGadget && blackboard.Power>0)
        {      
           
            blackboard.Power -= blackboard.WastePowerVelocityABSORB * Time.deltaTime;   
        }
        if(UsingGadget && blackboard.Power<0)
        {
            NoPower = true;  
        }
        if(blackboard.Power <= 100 && UsingGadget == false && m_CharacterController.isGrounded && Absorving == false) 
        {
            blackboard.Power += 1 * blackboard.ReloadPowerSpeed * Time.deltaTime;
            if(blackboard.Power>=100)
            {
                NoPower = false;
                blackboard.Power = 100;
            }

        }
    }
   
    public bool IsPackageFull()
    {
        if(blackboard.Gun.GetComponent<Aspiradora>().Biomass >= blackboard.Gun.GetComponent<Aspiradora>().MaxBiomass)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RestLife()
    {
        if(AfectedByTheGas == true)
        {
            if(blackboard.currentLife >= 0)
            {
                blackboard.currentLife -= blackboard.ResistanceToTheGas * Time.deltaTime;
            }
            
        }
    }
    public void SumLife(float x)
    {
        if(blackboard.currentLife < 100)
        {
            blackboard.currentLife += x;

            if (blackboard.currentLife > 100)
            {
                blackboard.currentLife = 100;
            }
        }
    } 
    public void PlayerReciveDamage(float lifeToRest)
    { 
        blackboard.BiomassObj.GetComponent<DamageBiomasIntaciate>().rotate = true;
        blackboard.currentLife = blackboard.currentLife - lifeToRest;        
    }

    void ReducePlayerHealth()
    {
        currentHealth -= Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, -1, maxHealth);
    }  

    public void DamageAtFall()
    {
        if(!m_CharacterController.isGrounded)
        {
            TimeAtFalling += 1*Time.deltaTime;
            if(TimeAtFalling>= 0.9)
            {
                Debug.Log("MORITE PUTO");
                PlayerReciveDamage(100);
            }           
        }
        else
        {
            TimeAtFalling = 0;
        }   
    } 
    
}

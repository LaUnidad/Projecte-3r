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

    [Header("COSAS AUTOMATICAS, NO TE RALLES TT")]
    public Animator anim;
    public BLACKBOARD_ThirdPersonCharacter blackboard;
    private CharacterController m_CharacterController;
    public HippieMovement hMovement;


    

    float maxHealth;

    [Header("A ESTAS VARIABLES NI CASO")]

    public bool UsingGadget;

    public bool NoPower;

    public bool AfectedByTheGas;

    bool StopLook;

    public bool Shootting;

    public bool Absorving;

    public bool WiningLife;
    public float currentHealth;  
    bool playerDead;


      
    [Header("CONO DE ABSORCIÓN!!")]
    public GameObject vaccumCone;
    

    
    
    
    

    

    void Awake()
    {
        maxHealth = 100;
        //currentHealth = maxHealth;

        restartPosition = transform.position;
        restartRotation = transform.rotation;
    }

    void Start()
    {
        hMovement = GetComponent<HippieMovement>();
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
        if ((Input.GetMouseButton(blackboard.m_Absorb) || Input.GetButton("Right Trigger")) && !NoPower)
        {
            UsingGadget = true;
            //blackboard.RotationSpeed = 0.2f;
            
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
            //blackboard.RotationSpeed = 1f;
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

        
        if(other.tag == "Enemy")
        {
            Vector3 hitDirection = transform.position - other.transform.position;
            hitDirection = hitDirection.normalized;
            hMovement.KnockBack(hitDirection, 2f);
            Debug.Log("Knocback COntroller");
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
            WiningLife = true; 
            //Invoke("StupidFunction", 2);
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
       // hMovement.KnockBack();
    }

    void ReducePlayerHealth()
    {
        currentHealth -= Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, -1, maxHealth);
    }  

    public void StupidFunction()
    {
        WiningLife = false;
    }
    
}

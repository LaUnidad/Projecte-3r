﻿using System.Collections;
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


    public BLACKBOARD_ThirdPersonCharacter blackboard;
    private CharacterController m_CharacterController;

    /////PLAYER
   // private float MovHor;
   // private float MovVer;
   // private Vector3 l_Movment;

   // private float VerticalSpeed;

    public bool UsingGadget;

    public bool NoPower;

    public bool AfectedByTheGas;

    //////CAMERA
   // public Camera cam;
   // private Vector3 camForward;
  //  private Vector3 CamRight;

   // public bool isOnSlope = false;

    public bool StopLook;

  //  private float ActualSpeed;
  //  private Vector3 hitNormal;

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
       //////////////////////////////////////CAPTURA DE INPUTS Y MOVIMIENTO/////////////////////////
      // MovHor = Input.GetAxis("Horizontal");
     //  MovVer = Input.GetAxis("Vertical");
     //  Vector3 orientation = new Vector3(MovHor,0,MovVer);
    //   orientation = Vector3.ClampMagnitude(orientation, 1);
       /////////////////////////////////////ROTACIÓN//////////////////////////////////////////////////
      //  CamDirection();
     //   l_Movment = orientation.x * CamRight + orientation.z * camForward;

        /*
        if(!StopLook)
        {
            m_CharacterController.transform.LookAt((m_CharacterController.transform.position + l_Movment));
        }
        else
        {
            if(blackboard.Gun.GetComponent<Aspiradora>().ObjectToLookAt() != null)
            {
                //Debug.Log("Mirando al OBJETO");
                m_CharacterController.transform.LookAt(blackboard.Gun.GetComponent<Aspiradora>().ObjectToLookAt().transform);
            }
            else
            {
                m_CharacterController.transform.LookAt((m_CharacterController.transform.position + l_Movment));
            }  
        }
       /////////////////////////////////////GRAVEDAD//////////////////////////////////////////////////
    //   SetGravity();
       DamageAtFall();
       //Debug.Log(TimeAtFalling + "   " + m_CharacterController.isGrounded);
       /////////////////////////////////////SALTO/////////////////////////////////////////////////////
       /*
       if((Input.GetKeyDown(blackboard.m_JumpCode) || Input.GetButtonDown("A")) && m_CharacterController.isGrounded)
       {   
           VerticalSpeed = blackboard.JumpForce/2f;
           l_Movment.y = VerticalSpeed;
       }
       else
       { 
           VerticalSpeed -= (blackboard.Gravity/3.5f) * Time.deltaTime;
           l_Movment.y = VerticalSpeed;
       }
      
       ////////////////////////////////////////CORRER Y VELOCIDAD///////////////////////////////////////////////
        if(Input.GetKey(blackboard.m_RunKeyCode))
        {   
           ActualSpeed = blackboard.RunSpeed;
        }
        else
        { 
           ActualSpeed = blackboard.WalkSpeed;
        }
        
        l_Movment = l_Movment * ActualSpeed * blackboard.ForceAtAbsorb;
        /////////////////////////////////////MOVIMIENTO/////////////////////////////////////////////////////////
        m_CharacterController.Move(l_Movment *Time.deltaTime);

        */
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
                }
                else
                {
                    StopLook = false;
                }      
            }  
            else
            {
               Absorving = false;
            }  
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
        }
        else
        {
            vaccumCone.SetActive(false);
        }
        ////////////////////////////////////////////////LIFE//////////////////////////////////////////////////////
        RestLife();
        
        if (blackboard.Life <= 0)
        {
            gameManager.RestartGame();
        }
        /*
        if (isDeadWorldActive)
        {
            ReducePlayerHealth();
        }

        if (currentHealth <= 0)
        {
            gameManager.RestartGame();
        }
        */
       // Debug.Log("Current health:  " + currentHealth);
       //////////////////////////////////////////////POWER///////////////////////////////////////////////////////
        UsePower();
       /////////////////////////////////////////////////DAMAGE///////////////////////////////////////////////////
       
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        this.transform.position = restartPosition;
        this.transform.rotation = restartRotation;
        //currentHealth = maxHealth;
        blackboard.Life = 100;
        playerDead = false;
        Debug.Log("MUERETE");
    }
    /*
    void CamDirection() 
    {
        camForward = cam.transform.forward;
        CamRight = cam.transform.right;
        camForward.y = 0;
        CamRight.y = 0;
        camForward = camForward.normalized;
        CamRight = CamRight.normalized;
    }
    void SetGravity()
    {
        if(m_CharacterController.isGrounded)
        {
            VerticalSpeed = -blackboard.Gravity * Time.deltaTime;
            l_Movment.y = VerticalSpeed;
        }
        else
        {
            VerticalSpeed -= (blackboard.Gravity/3.5f) * Time.deltaTime;
            l_Movment.y = VerticalSpeed;
        }
        //DESLIZAMIENTO//
        SlideDown();
    }
    */
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
    /*
    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal)>= m_CharacterController.slopeLimit;
        if(isOnSlope)
        {
            l_Movment.x += ((1f-hitNormal.y)*hitNormal.x) * blackboard.SlideDownSpeed;
            l_Movment.z += ((1f-hitNormal.y)*hitNormal.z) * blackboard.SlideDownSpeed;
            l_Movment.y += blackboard.SlideDownForce;
        }  
    }
    */
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
            if(blackboard.Life >= 0)
            {
                blackboard.Life -= blackboard.ResistanceToTheGas * Time.deltaTime;
            }
            
        }
    }
    public void SumLife(float x)
    {
        if(blackboard.Life < 100)
        {
            blackboard.Life += x;

            if (blackboard.Life > 100)
            {
                blackboard.Life = 100;
            }
        }
    } 
    public void PlayerReciveDamage(float lifeToRest)
    { 
        blackboard.BiomassObj.GetComponent<DamageBiomasIntaciate>().rotate = true;
        blackboard.Life = blackboard.Life - lifeToRest;        
        //currentHealth -= lifeToRest;
    }
    /*
    void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        hitNormal = hit.normal;
    }
    */
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

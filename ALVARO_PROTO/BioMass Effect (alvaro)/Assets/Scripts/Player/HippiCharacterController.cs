using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (BLACKBOARD_ThirdPersonCharacter))]
[RequireComponent(typeof (CharacterController))]
public class HippiCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public BLACKBOARD_ThirdPersonCharacter blackboard;
    private CharacterController m_CharacterController;

    /////PLAYER
    private float MovHor;
    private float MovVer;
    private Vector3 l_Movment;

    private float VerticalSpeed;

    public bool UsingGadget;

    public bool Jetpack;

    //////CAMERA
    public Camera cam;
    private Vector3 camForward;
    private Vector3 CamRight;

    public bool isOnSlope = false;

    public bool StopLook;



    private float ActualSpeed;
    private Vector3 hitNormal;

    public bool Absorving;

    public bool ICanAbsorbThis;

    public bool Shootting;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        blackboard = GetComponent<BLACKBOARD_ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
       //////////////////////////////////////CAPTURA DE INPUTS Y MOVIMIENTO/////////////////////////
       MovHor = Input.GetAxis("Horizontal");
       MovVer = Input.GetAxis("Vertical");
       Vector3 orientation = new Vector3(MovHor,0,MovVer);
       orientation = Vector3.ClampMagnitude(orientation, 1);
       /////////////////////////////////////ROTACIÓN//////////////////////////////////////////////////
        CamDirection();
        l_Movment = orientation.x * CamRight + orientation.z * camForward;
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
       SetGravity();
       /////////////////////////////////////SALTO/////////////////////////////////////////////////////
       if(Input.GetKey(blackboard.m_JumpCode) && blackboard.Power>0 && ICanAbsorbThis == false)
       {   
           Jetpack = true;
           VerticalSpeed = blackboard.JetpackForce;
           l_Movment.y = VerticalSpeed;
       }
       else
       { 
           Jetpack = false;
           VerticalSpeed -= blackboard.Gravity * Time.deltaTime;
           l_Movment.y = VerticalSpeed;
       }
      
       ////////////////////////////////////////CORRER Y VELOCIDAD//////////////////////////////////////////////////////////
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
         ///////////////////////////////////ABSORB/////////////////////////////////////////////////////////////
        if (Input.GetMouseButton(blackboard.m_Absorb) && ICanAbsorbThis == false && IsPackageFull() != true)
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
       ///////////////////////////////////SHOOT/////////////////////////////////////////////////////////////
        if (Input.GetMouseButtonDown(blackboard.m_Shoot) && ICanAbsorbThis)
        { 
            //Debug.Log("SHOOOOOOOOOOOOOOOT");
            Shootting = true;
        } 
        if (Input.GetMouseButtonUp(blackboard.m_Shoot))
        {
            Shootting = false;
        }
       //////////////////////////////////////////////POWER///////////////////////////////////////////////////////
        UsePower();
        //Debug.Log(IsPackageFull());
    }

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
            VerticalSpeed -= blackboard.Gravity * Time.deltaTime;
            l_Movment.y = VerticalSpeed;
        }
        //DESLIZAMIENTO//
        SlideDown();
    }
    void UsePower()
    {
        if(UsingGadget && blackboard.Power>0)
        {   
            if(blackboard.Power<=0)
            {
                blackboard.Power = 0;
            }
            else
            {
                blackboard.Power -= blackboard.WastePowerVelocityABSORB * Time.deltaTime;
            }
        }
        else if(Jetpack && blackboard.Power>0)
        {
             if(blackboard.Power<=0)
            {
                blackboard.Power = 0;
            }
            else
            {
                blackboard.Power -= blackboard.WastePowerVelocityJETPACK * Time.deltaTime;
            }
        }
        if(blackboard.Power <= 100 && UsingGadget == false && m_CharacterController.isGrounded && Absorving == false) 
        {
            blackboard.Power += 1 * blackboard.ReloadPowerSpeed * Time.deltaTime;
        }
    }
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




    void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        hitNormal = hit.normal;
    }

    
    
}

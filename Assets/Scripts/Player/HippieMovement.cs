using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BLACKBOARD_ThirdPersonCharacter))]
[RequireComponent(typeof(CharacterController))]

public class HippieMovement : MonoBehaviour
{
    GameManager gameManager;

    public Animator anim;

    public BLACKBOARD_ThirdPersonCharacter blackboard;
    private CharacterController m_CharacterController;
    public HippiCharacterController hippieController;

    /////INPUT
    private float InputX;
    private float InputZ;
    private Vector3 l_Movment;

    private float VerticalSpeed;

    public float Speed;

    //////CAMERA
    public Camera cam;
    private Vector3 camForward;
    private Vector3 CamRight;

    public bool isOnSlope = false;

    private float ActualSpeed;
    private Vector3 hitNormal;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        m_CharacterController = GetComponent<CharacterController>();
        blackboard = GetComponent<BLACKBOARD_ThirdPersonCharacter>();
        hippieController = GetComponent<HippiCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////////CAPTURA DE INPUTS Y MOVIMIENTO/////////////////////////
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        Vector3 orientation = new Vector3(InputX, 0, InputZ);
        orientation = Vector3.ClampMagnitude(orientation, 1);
        /////////////////////////////////////ROTACIÓN//////////////////////////////////////////////////
        CamDirection();
        l_Movment = orientation.x * CamRight + orientation.z * camForward;

        if (!hippieController.StopLook)
        {
            m_CharacterController.transform.LookAt((m_CharacterController.transform.position + l_Movment));
        }
        else
        {
            if (blackboard.Gun.GetComponent<Aspiradora>().ObjectToLookAt() != null)
            {
                //Debug.Log("Mirando al OBJETO");
                m_CharacterController.transform.LookAt(blackboard.Gun.GetComponent<Aspiradora>().ObjectToLookAt().transform);
            }
            else
            {
                m_CharacterController.transform.LookAt((m_CharacterController.transform.position + l_Movment));
            }
        }

        SetGravity();

        /////////////////////////////////////SALTO/////////////////////////////////////////////////////
        if ((Input.GetKeyDown(blackboard.m_JumpCode) || Input.GetButtonDown("A")) && m_CharacterController.isGrounded)
        {
            VerticalSpeed = blackboard.JumpForce / 2f;
            l_Movment.y = VerticalSpeed;
        }
        else
        {
            VerticalSpeed -= (blackboard.Gravity / 3.5f) * Time.deltaTime;
            l_Movment.y = VerticalSpeed;
        }

        ////////////////////////////////////////CORRER Y VELOCIDAD///////////////////////////////////////////////
        if (Input.GetKey(blackboard.m_RunKeyCode))
        {
            ActualSpeed = blackboard.RunSpeed;
        }
        else
        {
            ActualSpeed = blackboard.WalkSpeed;
        }

        l_Movment = l_Movment * ActualSpeed * blackboard.ForceAtAbsorb;
        /////////////////////////////////////MOVIMIENTO/////////////////////////////////////////////////////////
        m_CharacterController.Move(l_Movment * Time.deltaTime);

        InputMagnitude();
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
        if (m_CharacterController.isGrounded)
        {
            VerticalSpeed = -blackboard.Gravity * Time.deltaTime;
            l_Movment.y = VerticalSpeed;
        }
        else
        {
            VerticalSpeed -= (blackboard.Gravity / 3.5f) * Time.deltaTime;
            l_Movment.y = VerticalSpeed;
        }
        //DESLIZAMIENTO//
        SlideDown();
    }

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= m_CharacterController.slopeLimit;
        if (isOnSlope)
        {
            l_Movment.x += ((1f - hitNormal.y) * hitNormal.x) * blackboard.SlideDownSpeed;
            l_Movment.z += ((1f - hitNormal.y) * hitNormal.z) * blackboard.SlideDownSpeed;
            l_Movment.y += blackboard.SlideDownForce;
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    // ----- ANIMACIONS -----
    void InputMagnitude()
    {
        // Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        anim.SetFloat("InputZ", InputZ, 0.3f, Time.deltaTime * 2f);
        anim.SetFloat("InputX", InputX, 0.3f, Time.deltaTime * 2f);

        // Calculate Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;
        // ?? Speed = new Vector2(InputX, InputZ).normalized.sqrMagnitude;

        //Physically move player

        if (Speed >= 0.001)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
           // PlayerMoveAndRotation();
        }
        else if (Speed < 0.001)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
        }
    }
}

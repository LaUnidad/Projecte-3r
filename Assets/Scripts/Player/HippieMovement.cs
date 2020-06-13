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
    private Vector3 l_Movement;
    private Vector3 lastMove;


    private float VerticalSpeed;

    public float Speed;

    //////CAMERA
    public Camera cam;
    private Vector3 camForward;
    private Vector3 CamRight;

    public bool isOnSlope = false;

    private float ActualSpeed;
    private Vector3 hitNormal;

    public bool canMove;

    //knocback
    public float knockBackTime = 2f;
    private float knockBackCounter;

    public float gravityScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        m_CharacterController = GetComponent<CharacterController>();
        blackboard = GetComponent<BLACKBOARD_ThirdPersonCharacter>();
        hippieController = GetComponent<HippiCharacterController>();
        canMove = true;
        blackboard.RotationSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////////CAPTURA DE INPUTS Y MOVIMIENTO/////////////////////////
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        Vector3 orientation = new Vector3(InputX, 0, InputZ);
        orientation = Vector3.ClampMagnitude(orientation, 1);

        ///////////////////////////////////////////////GRAVITY///////////////////////////////////////////

        SetGravity();
        /////////////////////////////////////ROTACIÓN//////////////////////////////////////////////////
        CamDirection();
        //if (m_CharacterController.isGrounded) canMove = true;
        // else canMove = false;

        if (knockBackCounter <= 0 && canMove)
        {

            l_Movement = orientation.x * CamRight + orientation.z * camForward;
            m_CharacterController.transform.LookAt((m_CharacterController.transform.position + l_Movement * blackboard.RotationSpeed));


            //l_Movement = new Vector3(Input.GetAxis("Horizontal") * blackboard.NormalSpeed, l_Movement.y, Input.GetAxis("Vertical") * blackboard.NormalSpeed);
            /////////////////////////////////////SALTO/////////////////////////////////////////////////////
            if(m_CharacterController.isGrounded)
            {
                //l_Movement.y = 0f;
                if ((Input.GetKeyDown(blackboard.m_JumpCode) || Input.GetButtonDown("A")) && !hippieController.UsingGadget)
                {
                    Jump();
                }
            }           
            else
            {
                VerticalSpeed -= (blackboard.Gravity / 3.5f) * Time.deltaTime;
                l_Movement.y = VerticalSpeed;
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

            l_Movement = l_Movement * ActualSpeed * blackboard.ForceAtAbsorb;
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            // l_Movement = lastMove;
        }

        //l_Movement = l_Movement * ActualSpeed * blackboard.ForceAtAbsorb;
        /////////////////////////////////////MOVIMIENTO/////////////////////////////////////////////////////////
         //VerticalSpeed -= (blackboard.Gravity / 3.5f) * Time.deltaTime;
         //l_Movement.y = VerticalSpeed;
        //l_Movement.y = l_Movement.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        m_CharacterController.Move(l_Movement * Time.deltaTime * blackboard.NormalSpeed);

            InputMagnitude();        
    }

    void Jump()
    {
        anim.SetTrigger("Jump");
        VerticalSpeed = blackboard.JumpForce / 2f;
        l_Movement.y = VerticalSpeed;
    }

    public void KnockBack(Vector3 direction, float knockBackForce)
    {
        Debug.Log("Knockback");

        knockBackCounter = knockBackTime;
       // canMove = false;
        l_Movement = direction * knockBackForce;
        l_Movement.y = knockBackForce;
        //Jump();        
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
            l_Movement.y = VerticalSpeed;
        }
        else
        {
            VerticalSpeed -= (blackboard.Gravity / 3.5f) * Time.deltaTime;
            l_Movement.y = VerticalSpeed;
        }
        //DESLIZAMIENTO//
        SlideDown();
    }

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= m_CharacterController.slopeLimit;
        if (isOnSlope)
        {
            l_Movement.x += ((1f - hitNormal.y) * hitNormal.x) * blackboard.SlideDownSpeed;
            l_Movement.z += ((1f - hitNormal.y) * hitNormal.z) * blackboard.SlideDownSpeed;
            l_Movement.y += blackboard.SlideDownForce;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    // ----- ANIMATIONS -----
    void InputMagnitude()
    {
        // Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        float dampTime = 0.3f;

        anim.SetFloat("InputZ", InputZ, dampTime, Time.deltaTime * 2f);
        anim.SetFloat("InputX", InputX, dampTime, Time.deltaTime * 2f);

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

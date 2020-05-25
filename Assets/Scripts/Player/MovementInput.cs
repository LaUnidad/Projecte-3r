using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public Animator anim;
    public Camera cam;
    public CharacterController controller;

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed;
    public float Speed;
    public float allowPlayerRotation;
    public bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;


    //provisional
    public float movementSpeed = 3f;
    private float originalMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        controller = GetComponent<CharacterController>();

        originalMoveSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();

        isGrounded = controller.isGrounded;
        if(isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 2;
        }

        moveVector = new Vector3(0, verticalVel, 0);
        // ?? moveVector = new Vector3(0, verticalVel, 0).normalized;
        //controller.Move(moveVector);

        controller.Move(desiredMoveDirection * Time.deltaTime * movementSpeed);


    }

    // This is gonna move the player
    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if ( blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
    }

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

        if(Speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
        } 
    }






}

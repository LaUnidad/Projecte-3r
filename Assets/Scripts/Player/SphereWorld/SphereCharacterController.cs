using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCharacterController : MonoBehaviour
{

    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;

    public Transform cameraT;

    float verticalLookRotation;

    public float walkSpeed = 10f;
    public float jumpForce = 200f;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    Rigidbody rigidBody;

    bool grounded;
    public LayerMask groundedMask;

    // Start is called before the first frame update
    void Start()
    {
        cameraT = Camera.main.transform;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        if (Input.GetButtonDown("Jump"))
        {
            if(grounded)
            {
                rigidBody.AddForce(transform.up * jumpForce);
            }
        }

        grounded = false;
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
            grounded = true;
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}

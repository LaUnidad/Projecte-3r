using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    // public Animation anim;
    //public AnimationClip animClip;

    private Animator camAnim;
    private Transform cameraTransform;
    private Transform parentTransform;

    private Vector3 localRotation;
    private float cameraDistance = 10f;

    public float mouseSensitivity = 4f;
    public float scrollSensitivity = 2f;
    public float orbitDampening = 10f;
    public float scrollDampening = 8f;

    public bool cameraDisabled = false;

    // Camera Collision

    public LayerMask collisionMask;
    public bool collisionRayDebug = false;

    public float collisionOffset = .5f;
    public float adjustedDistance;


    // Start is called before the first frame update
    void Start()
    {
        camAnim = GetComponent<Animator>();
        cameraTransform = this.transform;
        parentTransform = this.transform.parent;

        Cursor.lockState = CursorLockMode.Locked;

       // anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.L)) anim.Play(animClip.name);

        //if (Input.GetKeyDown(KeyCode.LeftShift)) cameraDisabled = !cameraDisabled;

        if(!cameraDisabled)
        {
            if((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Right Stick Horizontal") != 0) || (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Right Stick Vertical") != 0))
            {
                localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
                localRotation.x += Input.GetAxis("Right Stick Horizontal") * mouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                localRotation.y += Input.GetAxis("Right Stick Vertical") * mouseSensitivity;

                //Clamp rotation angle
                localRotation.y = Mathf.Clamp(localRotation.y, -30f, 70f);
            }

            //Zoom with Mouse scroll
            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

                //Zoom faster the further away the camera is
                scrollAmount *= cameraDistance * 0.3f;

                cameraDistance += scrollAmount * -1f;

                cameraDistance = Mathf.Clamp(cameraDistance, 6f, 16f);

            }
        }

        Quaternion qt = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        parentTransform.rotation = Quaternion.Lerp(parentTransform.rotation, qt, Time.deltaTime * orbitDampening);

        if(cameraTransform.localPosition.z != adjustedDistance * -1f)
        {
            //cameraTransform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(cameraTransform.localPosition.z, cameraDistance * -1, Time.deltaTime * scrollDampening));
            cameraTransform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(cameraTransform.localPosition.z, adjustedDistance * -1, Time.deltaTime * scrollDampening));
        }

        CameraCollision();
    }

    void CameraCollision()
    {
        float camDistance = cameraDistance + collisionOffset;

        Ray camRay = new Ray(parentTransform.position, -cameraTransform.forward);
        RaycastHit camRayHit;

        if(Physics.Raycast(camRay, out camRayHit, camDistance, collisionMask))
        {
            adjustedDistance = Vector3.Distance(camRay.origin, camRayHit.point) - collisionOffset;
        }
        else
        {
            adjustedDistance = cameraDistance;
        }

        if (collisionRayDebug)
            Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction * cameraDistance, Color.cyan);

    }

    public void FinalCameraShakeStart()
    {
        //camAnim.SetBool("FinalCameraShake", true);
    }

    public void FinalCameraShakeStop()
    {
        //camAnim.SetBool("FinalCameraShake", false);
    }

}

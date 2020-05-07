using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMario : MonoBehaviour
{
    MovementInput m_PlayerController;

    float m_Yaw;
    float m_Pitch;
    public float m_YawRotationalSpeed = 360f;
    public float m_PitchRotationalSpeed = 180f;
    public float m_MinPitch = 80f;
    public float m_MaxPitch = 50f;
    public bool m_InvertedYaw = false;
    public bool m_InvertedPitch = false;

    private bool m_AngleLocked;

    private float m_OffsetOnCollision = .5f;
    public float m_MaxDistanceToLookAt;
    public float m_MinDistanceToLookAt;

    private float repositionTimer;

    public LayerMask m_RaycastLayerMask;

    public Transform m_LookAt; // mario Transform;
    public Transform m_repositionLookAt;

    Transform cameraTransform;

    // Use this for initialization
    void Start()
    {
        m_PlayerController = FindObjectOfType<MovementInput>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        float l_Distance = Vector3.Distance(transform.position, m_LookAt.position);
        Vector3 l_Direction = m_LookAt.position - transform.position;

        float l_MouseAxisX = Input.GetAxis("Mouse X");
        float l_MouseAxisY = Input.GetAxis("Mouse Y");

        Vector3 l_DesiredPosition = transform.position;

        if (!m_AngleLocked && (l_MouseAxisX > 0.01f || l_MouseAxisX < -0.01f || l_MouseAxisY > 0.01f || l_MouseAxisY < -0.01f))
        {
            Vector3 l_EulerAngles = transform.eulerAngles;
            float l_Yaw = (l_EulerAngles.y + 180.0f);
            float l_Pitch = l_EulerAngles.x;

            l_Yaw += m_YawRotationalSpeed * l_MouseAxisX * Time.deltaTime;
            l_Yaw *= Mathf.Deg2Rad;
            if (l_Pitch > 180.0f)
                l_Pitch -= 360.0f;
            l_Pitch += m_PitchRotationalSpeed * (-l_MouseAxisY) * Time.deltaTime;
            l_Pitch = Mathf.Clamp(l_Pitch, m_MinPitch, m_MaxPitch);
            l_Pitch *= Mathf.Deg2Rad;
            l_DesiredPosition = m_LookAt.position + new Vector3(Mathf.Sin(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance, Mathf.Sin(l_Pitch) * l_Distance, Mathf.Cos(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance);
            l_Direction = m_LookAt.position - l_DesiredPosition;
        }

        l_Direction /= l_Distance;

        if (l_Distance > m_MaxDistanceToLookAt)
        {
            l_DesiredPosition = m_LookAt.position - l_Direction * m_MaxDistanceToLookAt;
            l_Distance = m_MaxDistanceToLookAt;
        }
        if (l_Distance < m_MinDistanceToLookAt)
        {
            l_DesiredPosition = m_LookAt.position - l_Direction * m_MinDistanceToLookAt;
            l_Distance = m_MinDistanceToLookAt;
        }

        if (transform.position != l_DesiredPosition)
        {
            repositionTimer = 0f;
        }

        RaycastHit l_RaycastHit;
        Ray l_Ray = new Ray(m_LookAt.position, -l_Direction);
        if (Physics.Raycast(l_Ray, out l_RaycastHit, l_Distance, m_RaycastLayerMask.value))
            l_DesiredPosition = l_RaycastHit.point + l_Direction * m_OffsetOnCollision;

        repositionTimer += Time.deltaTime;
        //Debug.Log("RepositionTimer : " + repositionTimer);

        if (repositionTimer >= 5f)
        {
            transform.position = Vector3.Lerp(l_DesiredPosition, m_repositionLookAt.position, 0.5f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, m_repositionLookAt.rotation, 0.5f * Time.deltaTime);
        }

        else
        {
            transform.forward = l_Direction;
            transform.position = l_DesiredPosition;
        }
    }
}

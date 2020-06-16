using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM_Movment : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("OBJETO AL QUE MIRAS Y AL QUE ORBITAS")]
    public GameObject ObjectToLookAt;

    [Header("VELOCIDAD DE LA CAMARA")]
    public float RotationVelocity;

    [Header("CAMBIO DE SENTIDO (DE NORMAL VA ORARIAMENTE)")]
    public bool ChangeDirection;

    [Header("CAMBIO DE ROTACION (HORIZONTAL HACIA ARRIBA)")]
    public bool ChangeRotation;

    [Header("SI ''ChangeRotation' LO HACE HACIA ABAJO, SORRY Y ACTIVA ESTE")]
    public bool ChangeRotation2;
    
    

    private Vector3 Direction;

    [Header("TIEMPO DE VIDA DE LA CAMARA")]
    public float TimeToStayAlive;

    [Header("ORDEN (DEL 0 AL ...)")]

    public int Order;

    [Header("NI PUTO CASO A ESTO")]

    public bool Move;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeVelocity();
        if(Move)
        {
            Movment();
        }
       
    }

    public void Movment()
    {
        this.transform.LookAt(ObjectToLookAt.transform, Vector3.up);
        this.transform.RotateAround(ObjectToLookAt.transform.position, Direction, RotationVelocity * Time.deltaTime);
    }

    public void ChangeVelocity()
    {
        if(!ChangeRotation2)
        {
            if(!ChangeRotation)
            {
                if(ChangeDirection)
                {
                    Direction = -Vector3.up;
                }
                else
                {
                    Direction = Vector3.up;
                }
            }
            else
            {
                Direction = -Vector3.forward;
            }
        }
        else
        {
            Direction = Vector3.forward;
        }
        
        
    }
}

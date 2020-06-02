using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM_Movment : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectToLookAt;

    public float RotationVelocity;

    public bool ChangeDirection;

    public bool ChangeRotation;

    private Vector3 Direction;

    public float TimeToStayAlive;

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
}

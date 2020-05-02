using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]
[RequireComponent(typeof (BLACKBOARD_ThirdPersonCharacter))]
public class ThirdPersonPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController m_CharacterController;
    private Quaternion targetrotation;

    private BLACKBOARD_ThirdPersonCharacter blackboard;

    public float actualSpeed;

    public float m_VerticalSpeed;
    

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        blackboard = GetComponent<BLACKBOARD_ThirdPersonCharacter>();
        actualSpeed = blackboard.WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {

       

        ////////////////////////////////////////// ROTATION OF THE PLAYER ////////////////////////////////////////////////////////
        
        Vector3 motion = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        
        if(motion != Vector3.zero)
        {
            targetrotation = Quaternion.LookRotation(motion);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetrotation.eulerAngles.y,450 * Time.deltaTime);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////// MOVMENT //////////////////////////////////////////////////////////////////////
        
            if(Input.GetKey(blackboard.m_RunKeyCode))
            {
                actualSpeed = blackboard.RunSpeed;
            }
            else
            {
                actualSpeed = blackboard.WalkSpeed;
            }

            m_CharacterController.Move(motion * actualSpeed * Time.deltaTime);
        
       
       
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       
      
        
       
       


       

    }

    
}

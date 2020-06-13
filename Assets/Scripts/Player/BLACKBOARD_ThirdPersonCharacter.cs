using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLACKBOARD_ThirdPersonCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("VARIABLES DE MOVIMIENTO BÁSICAS:")]
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpForce;
    public float SlideDownSpeed;
    public float SlideDownForce;
    public float KnockBackForce;
    public float RotationSpeed;
    public float NormalSpeed;
    public bool RoketMan;

    

    [Header("VARIABLES DE VIDA:")]
    public float currentLife;
    public float MaxLife = 100;
    public float ResistanceToTheGas;

    [Header("GRAVEDAD DEL PLANETA:")]
    public float Gravity;

    [Header("VARIABLES DE ABSORCIÓN:")]
    
    public float Power;
    public float WastePowerVelocityABSORB;
    public float ReloadPowerSpeed;

    public float ForceAtAbsorb;

    public float Biomassa;

    
    [Header("CONTROLES BÁSICOS:")]
    public KeyCode m_RunKeyCode = KeyCode.LeftShift;
    public KeyCode m_JumpCode = KeyCode.Space;

    public int m_Absorb = 0;
    public int m_Shoot = 1;
    public float m_ControllerTrigger;

    
    [Header("COSAS AUTOMÁTICAS:")]
    public GameObject Gun;

    public GameObject Obj;

    public GameObject BiomassObj;

    

    
    
    void Start()
    {
        Gun = GameObject.FindGameObjectWithTag("Gun");
        BiomassObj = GameObject.FindGameObjectWithTag("InstantiateBiomass");
        currentLife = MaxLife;
        
    }

    // Update is called once per frame
    void Update()
    {
        m_ControllerTrigger = Input.GetAxis("Right Trigger");
    }

    public bool ControllerAbsorb()
    {
        return m_ControllerTrigger == 1;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLACKBOARD_ThirdPersonCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public float WalkSpeed;
    public float RunSpeed;
    public float SlideDownSpeed;

    public float SlideDownForce;
    public float Gravity;
    public KeyCode m_RunKeyCode = KeyCode.LeftShift;
    public KeyCode m_JumpCode = KeyCode.Space;
    public int m_Absorb = 0;
    public int m_Shoot = 1;
    public float JumpForce;
    public float Power;

    public float currentLife;
    public float MaxLife = 100;

    public float ResistanceToTheGas;

    public float WastePowerVelocityABSORB;
    public float WastePowerVelocityJETPACK;

    public float ReloadPowerSpeed;

    public GameObject Gun;

    public GameObject Obj;

    public float ForceAtAbsorb;

    public GameObject BiomassObj;

    public float KnockBackForce;

    public bool RoketMan;

    
    
    void Start()
    {
        Gun = GameObject.FindGameObjectWithTag("Gun");
        BiomassObj = GameObject.FindGameObjectWithTag("InstantiateBiomass");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

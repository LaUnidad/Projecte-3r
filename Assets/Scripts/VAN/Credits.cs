﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    public bool credits;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(credits)
        {
            animator.SetTrigger("Credits");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimations : MonoBehaviour
{
    Animator camAnimator;

    // Start is called before the first frame update
    void Start()
    {
        camAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShakeOnce()
    {
        camAnimator.SetTrigger("CamShakeOnce");
    }

    public void FinalCameraShakeStart()
    {
        camAnimator.SetBool("FinalCameraShake", true);
    }

    public void FinalCameraShakeStop()
    {
        camAnimator.SetBool("FinalCameraShake", false);
    }
}

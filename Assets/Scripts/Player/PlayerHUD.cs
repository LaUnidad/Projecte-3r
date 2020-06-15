using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    GameManager gm;

    public Animator canvasAnimator;
    public Animator playerHUDAnimator;

    HippiCharacterController cc;

    public Image healthBar;

    public Image YellowhealthBar;
    public GameObject pauseScreen;

   // public ParticleSystem gasBubblesHUD;
    public GameObject gasBubblesHUD;

    public float OtherLife;

    public float VelocityRedLife;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<HippiCharacterController>();
        gm = FindObjectOfType<GameManager>();
        OtherLife = cc.blackboard.currentLife;

        gasBubblesHUD.SetActive(false);

        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ActAsMe();

        
        healthBar.fillAmount = OtherLife/100;
        YellowhealthBar.fillAmount = cc.blackboard.currentLife/100;

        if (cc.AfectedByTheGas) gasBubblesHUD.SetActive(true); //gasBubblesHUD.Play();

       IsPaused();
    }
    public void ActAsMe()
    {
        if(!cc.WiningLife)
        {
            OtherLife = cc.blackboard.currentLife;
        }
        else
        {
            OtherLife += 1 *Time.deltaTime * VelocityRedLife;

            if(OtherLife>= cc.blackboard.currentLife)
            {
                cc.WiningLife = false;
            }
        }
    }

    public void IsPaused()
    {
        if(gm.isPaused)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }
    }

    public void ShowDeathFadeIn()
    {
        canvasAnimator.SetTrigger("DeathFadeIn");
    }

    public void ShowAliveFadeOut()
    {
        canvasAnimator.SetTrigger("AliveFadeOut");
    }

    public void KnockBackHUDandDamageVignette()
    {
        playerHUDAnimator.SetTrigger("HUDKnockback");
        canvasAnimator.SetTrigger("DamageVignette");
    }

    public void HitHUD()
    {
        canvasAnimator.SetTrigger("Hit");
    }

    public void SetLowHealthTrue()
    {
        canvasAnimator.SetBool("LowHealthWarning", true);
    }

    public void SetLowHealthFalse()
    {
        canvasAnimator.SetBool("LowHealthWarning", false);
    }

}

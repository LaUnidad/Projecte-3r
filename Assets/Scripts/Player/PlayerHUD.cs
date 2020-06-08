using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    GameManager gm;

    HippiCharacterController cc;

    public Image healthBar;

    public Image YellowhealthBar;
    public GameObject pauseScreen;

    public float OtherLife;

    public float VelocityRedLife;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<HippiCharacterController>();
        gm = FindObjectOfType<GameManager>();
        OtherLife = cc.blackboard.currentLife;

        //healthBar.fillAmount = cc.currentHealth / cc.maxHealth;

        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ActAsMe();

        
        healthBar.fillAmount = OtherLife/100;
        YellowhealthBar.fillAmount = cc.blackboard.currentLife/100;
       


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
}

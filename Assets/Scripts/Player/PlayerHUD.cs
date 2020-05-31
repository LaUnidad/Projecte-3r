using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    GameManager gm;

    HippiCharacterController cc;

    public Image healthBar;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<HippiCharacterController>();
        gm = FindObjectOfType<GameManager>();

        healthBar.fillAmount = cc.currentHealth / cc.maxHealth;

        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = cc.currentHealth / cc.maxHealth;

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

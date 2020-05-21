using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image healthBar;

    HippiCharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<HippiCharacterController>();


        healthBar.fillAmount = cc.currentHealth / cc.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = cc.currentHealth / cc.maxHealth;
    }
}

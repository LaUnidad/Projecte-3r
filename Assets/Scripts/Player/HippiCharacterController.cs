using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using XInputDotNetPure;

[RequireComponent(typeof (BLACKBOARD_ThirdPersonCharacter))]
[RequireComponent(typeof (CharacterController))]
public class HippiCharacterController : MonoBehaviour, IRestartGameElement
{
    GameManager gameManager;

    // ----- RESTART -----
    Vector3 restartPosition;
    Quaternion restartRotation;

    [Header("COSAS AUTOMATICAS, NO TE RALLES TT")]
    public Animator anim;
    private PlayerHUD pHud;
    private CameraOrbit camOrbit;
    private CamAnimations camAnimations;
    private ChangeToAngryWorld changeToAngry;
    public BLACKBOARD_ThirdPersonCharacter blackboard;
    private CharacterController m_CharacterController;
    public HippieMovement hMovement;

    [Header("A ESTAS VARIABLES NI CASO")]

    public bool UsingGadget;
    public bool NoPower;
    public bool AfectedByTheGas;
    bool StopLook;
    public bool Shootting;
    public bool Absorving;

    public bool WiningLife;
    public float currentHealth;  
    bool playerIsDead;
      
    [Header("CONO DE ABSORCIÓN!!")]
    public GameObject vaccumCone;


    //----- HIT STOP -----
    bool waitingHitStop = false;

    //----- SOUNDS -----
    bool firstTimeUsingGadget;
    EventInstance AbsorbSoundEvent;
    EventInstance DamageSound, HealSound;

    //----- CONTROLLER -----
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;


    //------------Bools for Conditions (TUTO) ------

    public bool magneticRock;

    public bool Oasis;

    public bool killingPlanet;

    void Awake()
    {
        restartPosition = transform.position;
        restartRotation = transform.rotation;
    }

    void Start()
    {
        hMovement = GetComponent<HippieMovement>();
        pHud = GetComponent<PlayerHUD>();
        camOrbit = FindObjectOfType<CameraOrbit>();
        camAnimations = FindObjectOfType<CamAnimations>();
        changeToAngry = FindObjectOfType<ChangeToAngryWorld>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddRestartGameElement(this);

        m_CharacterController = GetComponent<CharacterController>();
        blackboard = GetComponent<BLACKBOARD_ThirdPersonCharacter>();
        Cursor.visible = false;

        vaccumCone.SetActive(false);
        AfectedByTheGas = false;
        playerIsDead = false;
    }

    // Update is called once per frame
    void Update()
    {       
        ///////////////////////////////////ABSORB/////////////////////////////////////////////////////////////
        if ((Input.GetMouseButton(blackboard.m_Absorb) || blackboard.ControllerAbsorb()) && !NoPower && !playerIsDead)
        {
            UsingGadget = true;
            blackboard.RotationSpeed = 0f;
            blackboard.NormalSpeed = 0.2f;
            
            if(blackboard.Power >= 0)
            {
                if(blackboard.Gun.GetComponent<Aspiradora>().ListObjects != 0)
                {
                    Absorving = true;
                    blackboard.ForceAtAbsorb = blackboard.Gun.GetComponent<Aspiradora>().ForceToThePlayer();
                    StopLook = true;               
                }
                else
                {
                    StopLook = false;
                }      
            }  
            else
            {
               Absorving = false;
            }
        }
        else
        {
            UsingGadget = false;
            Absorving = false;
            blackboard.ForceAtAbsorb = 1;
            StopLook = false;
            blackboard.NormalSpeed = 1f;
            blackboard.RotationSpeed = 1f;
        }

        if (UsingGadget && !playerIsDead)
        {
            vaccumCone.SetActive(true);
            anim.SetBool("Absorbing", true);

            if (!SoundManager.Instance.isPlaying(AbsorbSoundEvent)) AbsorbSoundEvent = SoundManager.Instance.PlayEvent(GameManager.Instance.Absorb, transform);
            firstTimeUsingGadget = false;

            GamePad.SetVibration(playerIndex, .05f, .05f); 
        }

        else
        {
            vaccumCone.SetActive(false);
            anim.SetBool("Absorbing", false);

            GamePad.SetVibration(playerIndex, 0f, 0f);

            AbsorbSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            firstTimeUsingGadget = true;
        }
        ////////////////////////////////////////////////LIFE//////////////////////////////////////////////////////
        RestLife();

        LowHealthIndicator();

        if (blackboard.currentLife <= 0)
        {
            playerIsDead = true;
            PlayerHasDied();
        }
      
       //////////////////////////////////////////////POWER///////////////////////////////////////////////////////
        UsePower();
       /////////////////////////////////////////////////DAMAGE///////////////////////////////////////////////////      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            restartPosition = other.transform.position;
            restartRotation = other.transform.rotation;
        }
      
        if(other.tag == "Enemy")
        {
            Vector3 hitDirection = transform.position - other.transform.position;
            hitDirection = hitDirection.normalized;
            hMovement.KnockBack(hitDirection, 2f);
            Debug.Log("Knocback COntroller");
        }       
    }

    

    void UsePower()
    {
        if(UsingGadget && blackboard.Power>0)
        {      
            blackboard.Power -= blackboard.WastePowerVelocityABSORB * Time.deltaTime;   
        }
        if(UsingGadget && blackboard.Power<0)
        {
            SoundManager.Instance.PlayOneShotSound(GameManager.Instance.AbsorbOverheat, GameManager.Instance.m_player.transform);
            NoPower = true;  
        }
        if(blackboard.Power <= 100 && UsingGadget == false && m_CharacterController.isGrounded && Absorving == false) 
        {
            blackboard.Power += 1 * blackboard.ReloadPowerSpeed * Time.deltaTime;
            
            if(blackboard.Power>=100)
            {
                NoPower = false;
                blackboard.Power = 100;
            }
        }
    }
   
    public bool IsPackageFull()
    {
        if(blackboard.Gun.GetComponent<Aspiradora>().Biomass >= blackboard.Gun.GetComponent<Aspiradora>().MaxBiomass)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RestLife()
    {
        if(AfectedByTheGas == true)
        {
            if(blackboard.currentLife >= 0)
            {
                blackboard.currentLife -= blackboard.ResistanceToTheGas * Time.deltaTime;

            }          
        }
    }
    public void SumLife(float x)
    {
        if(blackboard.currentLife < 100)
        {
            blackboard.currentLife += x;
            WiningLife = true; 
            //Invoke("StupidFunction", 2);
            if (blackboard.currentLife > 100)
            {
                blackboard.currentLife = 100;
            }
        }
    } 
    public void PlayerTakeDamage(float lifeToRest)
    { 
        blackboard.BiomassObj.GetComponent<DamageBiomasIntaciate>().rotate = true;
        blackboard.currentLife = blackboard.currentLife - lifeToRest;

        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.playerHit, transform);
        //FindObjectOfType<HitStop>().HitStopLoL(.1f);
        anim.SetTrigger("Knockback");
        // hMovement.KnockBack();

        camAnimations.CameraShakeOnce();
        pHud.KnockBackHUDandDamageVignette();
        pHud.HitHUD();
    }

    public void LowHealthIndicator()
    {
        if (blackboard.currentLife < 30 && !playerIsDead) pHud.SetLowHealthTrue();
        else pHud.SetLowHealthFalse();
    }

    /*
    void ReducePlayerHealth()
    {
        currentHealth -= Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, -1, maxHealth);
    }  
    */

    public void StupidFunction()
    {
        WiningLife = false;
    }

    public void RestartGame()
    {
        Debug.Log("Restart Player");
        Time.timeScale = 1f;
        this.transform.position = restartPosition;
        this.transform.rotation = restartRotation;
        blackboard.currentLife = blackboard.MaxLife;
        m_CharacterController.enabled = true;
        anim.SetBool("DeathBySuffocation", false);
        pHud.ShowAliveFadeOut();
        hMovement.canMove = true;
        NoPower = false;     
        playerIsDead = false;

        if (changeToAngry.tutorialCompleted) AfectedByTheGas = true;
        else AfectedByTheGas = false;

    }

    public void PlayerHasDied()
    {
       // playerIsDead = false;
        hMovement.canMove = false;
        m_CharacterController.enabled = false;
        blackboard.currentLife = 0.1f;
        AfectedByTheGas = false;
        NoPower = true;
        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.playerDie, GameManager.Instance.m_player.transform);
        anim.SetBool("DeathBySuffocation", true);
        pHud.ShowDeathFadeIn();
        
        StartCoroutine(WaitAndRetry(5f));
    }

    private IEnumerator WaitAndRetry(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_CharacterController.enabled = false;
        gameManager.RestartGame();
    }
    /*
    void HitStop(float duration)
    {
        if (waitingHitStop)
            return;
        Time.timeScale = 0f;
        StartCoroutine(WaitHitStop(duration));
    }

    IEnumerator WaitHitStop(float duration)
    {
        waitingHitStop = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        waitingHitStop = false;
    }
    */

}

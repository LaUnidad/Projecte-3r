using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public interface IRestartGameElement
{
    void RestartGame();
}

public class GameManager : MonoBehaviour
{
    public GameObject m_player;

    HippiCharacterController cc;

    private static GameManager _instance;

    List<IRestartGameElement> m_RestartGameElements = new List<IRestartGameElement>();
    bool m_GameActive = true;


    public float RotateSpeed = 1f;
    public Animation m_Animation;
    public AnimationClip m_ShowHUDAnimationClip;
    public GameObject pauseMenu;
    public bool isPaused;

    private AspirableObject[] m_AspirableObject;

    [Header("Sound Bank")]
    //CHARACTER
    public string stepGrass = "event:/FX/Character/StepGrass";
    public string stepGround = "event:/FX/Character/StepGround";
    public string playerHit = "event:/FX/Character/Hit";
    public string playerDie = "event:/FX/Character/Die";
    public string playerJump = "event:/FX/Character/Jump";
    public string playerLand = "event:/FX/Character/Land";
    public string ChangeDirection = "event:/FX/Character/ChangeDirection";
    //ENEMIES
    //ENEMY 1
    public string E1_Down = "event:/FX/Enemies/Enemy1/Down";
    public string E1_Twincle = "event:/FX/Enemies/Enemy1/Twinkle";
    public string E1_Up = "event:/FX/Enemies/Enemy1/Up";
    //ENEMY 2
    public string E2_Close = "event:/FX/Enemies/Enemy2/Close";
    public string E2_Open = "event:/FX/Enemies/Enemy2/Open";
    public string E2_Rotate = "event:/FX/Enemies/Enemy2/Rotate";
    public string E2_Shoot = "event:/FX/Enemies/Enemy2/Shoot";
    public string E2_Wander = "event:/FX/Enemies/Enemy2/Wander";
    //ENEMY 3
    public string E3_Attack = "event:/FX/Enemies/Enemy3/Attack";
    public string E3_Fly = "event:/FX/Enemies/Enemy3/Fly";
    public string E3_Yell = "event:/FX/Enemies/Enemy3/Yell";
    //HUD
    public string Damage = "event:/FX/HUD/Damage";
    public string Heal = "event:/FX/HUD/Heal";
    //MENU
    public string Accept = "event:/FX/Menu/Accept";
    public string Button = "event:/FX/Menu/Button";
    public string Cancel = "event:/FX/Menu/Cancel";
    public string Exit = "event:/FX/Menu/Exit";
    public string PauseSound = "event:/FX/Menu/Pause";
    public string Resume = "event:/FX/Menu/Resume";
    //OBJECTS
    public string AbsorbableBig = "event:/FX/Objects/AbsorbableBig";
    public string AbsorbableNormal = "event:/FX/Objects/AbsorbableNormal";
    public string AbsorbableSmall = "event:/FX/Objects/AbsorbableSmall";
    public string GasRock = "event:/FX/Objects/GasRock";
    public string GasRockAbsorb = "event:/FX/Objects/GasRockAbsorb";
    public string GasRockMoving = "event:/FX/Objects/GasRockMoving";
    public string GasRockThrow = "event:/FX/Objects/GasRockThrow";
    public string GasSource = "event:/FX/Objects/GasSource";
    public string ObstacleHard = "event:/FX/Objects/ObstacleHard";
    public string ObstacleLight = "event:/FX/Objects/ObstacleLight";
    //WEAPON
    public string Absorb = "event:/FX/Weapon/Absorb";
    public string AbsorbCooldown = "event:/FX/Weapon/AbsorbCooldown";
    public string AbsorbOverheat = "event:/FX/Weapon/AbsorbOverheat";
    public string AbsorbWarning = "event:/FX/Weapon/AbsorbWarning";
    public string AbsorbWhileCooldown = "event:/FX/Weapon/AbsorbWhileCooldown";
    //TUTORIAL
    public string Audio_1 = "event:/FX/Tutorial/Audio_1";
    public string Audio_2 = "event:/FX/Tutorial/Audio_2";
    public string Audio_3 = "event:/FX/Tutorial/Audio_3";
    public string Audio_4 = "event:/FX/Tutorial/Audio_4";
    public string Audio_5 = "event:/FX/Tutorial/Audio_5";
    public string Audio_6 = "event:/FX/Tutorial/Audio_6";
    public string Audio_7 = "event:/FX/Tutorial/Audio_7";
    public string Audio_8 = "event:/FX/Tutorial/Audio_8";
    public string Audio_9_10 = "event:/FX/Tutorial/Audio9_10";
    public string Audio_11 = "event:/FX/Tutorial/Audio_11";
    public string Audio_12 = "event:/FX/Tutorial/Audio_12";
    public string Audio_13 = "event:/FX/Tutorial/Audio_13";
    public string Audio_14 = "event:/FX/Tutorial/Audio_14";
    public string Audio_15_16 = "event:/FX/Tutorial/Audio15_16";
    public string Audio_17 = "event:/FX/Tutorial/Audio_17";
    public string Audio_18 = "event:/FX/Tutorial/Audio_18";
    public string Audio_19 = "event:/FX/Tutorial/Audio_19";
    //AMBIENT
    public string Sea = "event:/FX/Objects/Bubble";
    public string Heart = "event:/FX/Objects/Heat";
    public string Wind = "event:/Music/ColdWind";
    public string Earthquake = "event:/Music/Earthquake";
    public string Pajaritos = "event:/Music/Pajaritos";
    public string VientoBosque = "event:/Music/VientoBosque";
    //MUSIC
    public string MainMenu = "event:/Music/MainMenu";
    public string PauseMusic = "event:/Music/Pause";
    public string Zone1 = "event:/Music/Zone1";
    public string Zone2 = "event:/Music/Zone2";
    public string Intro = "event:/Music/IntroSong";
    public string NightNurse = "event:/Music/NightNurse";
    public string RocketMan = "event:/Music/RocketMan";
    public string TakeMeHome = "event:/Music/TakeMeHome";

    private FMOD.Studio.EventInstance m_Intro, m_MZone1, m_MZone2;
    public FMOD.Studio.EventInstance earthquake;


    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        m_AspirableObject = (AspirableObject[]) FindObjectsOfType(typeof(AspirableObject));

        foreach (AspirableObject l_aspirableObject in m_AspirableObject)
        {

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        cc = FindObjectOfType<HippiCharacterController>();
        m_Intro = SoundManager.Instance.PlayEvent(TakeMeHome, transform);
        StartCoroutine(StartGameSong());
        //Musica Cinematica
    }

    // Update is called once per frame
    void Update()
    {
        

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);

        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Start"))
        {
            Pause();
            SoundManager.Instance.PlayOneShotSound(PauseSound, m_player.transform);

        }

       // if (cc.currentHealth <= 0) m_GameActive = false;
    }

    public void DamageHUD()
    {
        m_Animation.Play(m_ShowHUDAnimationClip.name);
        /*
        AnimationState l_AnimationState = m_Animation[m_ShowHUDAnimationClip.name];
        if (!l_AnimationState.enabled || l_AnimationState.normalizedTime >= 1.0f)
        {
            m_Animation.Stop();
            m_Animation.Play(m_ShowHUDAnimationClip.name);
        }*/
    }

    public void RestartGame()
    {
        foreach (IRestartGameElement l_RestartGameElement in m_RestartGameElements)
            l_RestartGameElement.RestartGame();
        m_GameActive = true;
    }
    public void AddRestartGameElement(IRestartGameElement RestartGameElement)
    {
        m_RestartGameElements.Add(RestartGameElement);
    }


    public void Pause()
    {
        
        isPaused = !isPaused;
        

        if (isPaused)
        {
            SoundManager.Instance.PauseAllEvents();
            Time.timeScale = 0f;
            pauseMenu.GetComponent<PauseMenu>().Pause();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            SoundManager.Instance.ResumeAllEvents();
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    IEnumerator StartGameSong()
    {
        yield return new WaitForSeconds(37f);
        m_Intro.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        m_MZone1 = SoundManager.Instance.PlayEvent(Zone1, transform);
    }

    public void ChangeSong()
    {
        m_MZone1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        m_MZone2 = SoundManager.Instance.PlayEvent(Zone2, transform);
    }

    public void StopSong()
    {
        m_MZone2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void FinalRun()
    {
        SoundManager.Instance.StopAllEvents(true);
        earthquake = SoundManager.Instance.PlayEvent(Earthquake, transform);
    }

    //Sounds Menu
    public void SoundAccept()
    {
        SoundManager.Instance.PlayOneShotSound(Accept, transform);
    }

    public void SoundResume()
    {
        SoundManager.Instance.PlayOneShotSound(Resume, transform);
    }

    public void SoundExit()
    {
        SoundManager.Instance.PlayOneShotSound(Exit, transform);
    }




}

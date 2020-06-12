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

    [Header("Sound Bank")]
    public string stepGrass = "event:/FX/Character/StepGrass";
    public string stepGround = "event:/FX/Character/StepGround";
    public string playerHit = "event:/FX/Character/Hit";
    public string playerDie = "event:/FX/Character/Die";
    public string Damage = "event:/FX/HUD/Damage";
    public string Heal = "event:/FX/HUD/Heal";
    public string AbsorbableBig = "event:/FX/Objects/AbsorbableBig";
    public string AbsorbableNormal = "event:/FX/Objects/AbsorbableNormal";
    public string AbsorbableSmall = "event:/FX/Objects/AbsorbableSmall";
    public string GasRock = "event:/FX/Objects/GasRock";
    public string GasRockThrow = "event:/FX/Objects/GasRockThrow";
    public string GasSource = "event:/FX/Objects/GasSource";
    public string ObstacleHard = "event:/FX/Objects/ObstacleHard";
    public string ObstacleLight = "event:/FX/Objects/ObstacleLight";
    public string Absorb = "event:/FX/Weapon/Absorb";
    public string AbsorbCooldown = "event:/FX/Weapon/AbsorbCooldown";
    public string AbsorbOverheat = "event:/FX/Weapon/AbsorbOverheat";
    public string AbsorbWarning = "event:/FX/Weapon/AbsorbWarning";
    public string ChangeDirection = "event:/FX/Character/ChangeDirection";



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

    // Start is called before the first frame update
    void Start()
    {
        cc = FindObjectOfType<HippiCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);

        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Start"))
        {
            Pause();
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
        }
        else
        {
            SoundManager.Instance.ResumeAllEvents();
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }

   
}

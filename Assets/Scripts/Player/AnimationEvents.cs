using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AnimationEvents : MonoBehaviour
{
    public GameObject runDustParticles;

    void RunStep()
    {
        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGround, GameManager.Instance.m_player.transform);
        Instantiate(runDustParticles, transform.position, runDustParticles.transform.rotation);
    }
    /*
    void RunStepL()
    {
        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGrass, GameManager.Instance.m_player.transform);

    }
    */
    void GrassStep()
    {

    }

    void RockStep()
    {

    }
}

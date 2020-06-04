using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AnimationEvents : MonoBehaviour
{
    void GrassStep()
    {
        SoundManager.Instance.PlayOneShotSound(GameManager.Instance.stepGrass, GameManager.Instance.m_player.transform);
    }


}

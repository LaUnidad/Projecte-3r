using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{

    public string RocketMan = "event:/Music/RocketMan";
    public FMOD.Studio.EventInstance rocketManSong;

    // Start is called before the first frame update
    void Start()
    {
        rocketManSong = SoundManager.Instance.PlayEvent(RocketMan, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSong()
    {
        rocketManSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}

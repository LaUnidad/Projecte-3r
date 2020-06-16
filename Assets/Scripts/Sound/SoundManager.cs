using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;

public class SoundManager : MonoBehaviour 
{

    public List<EventInstance> eventList;
    private List<SoundManagerMovingSound> positionEvents; 

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        eventList = new List<EventInstance>();
        positionEvents = new List<SoundManagerMovingSound>();
    }

    void Update()
    {

        //Actualitzar les posicions dels sons 3D
        if (positionEvents != null && positionEvents.Count > 0)
        {
            for (int i = 0; i < positionEvents.Count; i++)
            {
                PLAYBACK_STATE state;
                EventInstance eventInst = positionEvents[i].GetEventInstance();
                eventInst.getPlaybackState(out state);
                if (state == PLAYBACK_STATE.STOPPED)
                {
                    positionEvents.RemoveAt(i);
                }
                else
                {
                    eventInst.set3DAttributes(To3DAttributes(positionEvents[i].GetTransform().position));
                }
            }
        }
    }

    public void PlayOneShotSound(string path, Transform transform, bool low = false, float vol = 0f)
    {
        EventInstance soundEvent = RuntimeManager.CreateInstance(path);
        if (low) soundEvent.setVolume(vol);
        if (!soundEvent.Equals(null))
        {
            soundEvent.set3DAttributes(To3DAttributes(transform.position));
            soundEvent.start();
            SoundManagerMovingSound movingSound = new SoundManagerMovingSound(transform, soundEvent);
            positionEvents.Add(movingSound);
            soundEvent.release();
        }
    }

    // Utilitzar per a objectes estàtics
    public EventInstance PlayEvent(string path, Vector3 pos)
    {
        EventInstance soundEvent = RuntimeManager.CreateInstance(path);
        if (!soundEvent.Equals(null))
        {
            soundEvent.set3DAttributes(To3DAttributes(pos));
            soundEvent.start();
            eventList.Add(soundEvent);
        }

        return soundEvent;
    }

    // Utilitzar per a objectes en moviment
    public EventInstance PlayEvent(string path, Transform transform)
    {
        EventInstance soundEvent = RuntimeManager.CreateInstance(path);
        if (!soundEvent.Equals(null))
        {
            soundEvent.set3DAttributes(To3DAttributes(transform.position));
            soundEvent.start();
            SoundManagerMovingSound movingSound = new SoundManagerMovingSound(transform, soundEvent);
            positionEvents.Add(movingSound);
            eventList.Add(soundEvent);
        }

        return soundEvent;
    }

    public void StopEvent(EventInstance soundEvent, bool fadeout)
    {
        soundEvent.clearHandle();
        if (eventList.Remove(soundEvent))
        {
            if (fadeout)
                soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            else
                soundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void PauseEvent (EventInstance soundEvent)
    {
        if (eventList.Contains(soundEvent))
        {
            soundEvent.setPaused(true);
        }
    }

    public void ResumeEvent(EventInstance soundEvent)
    {
        if (eventList.Contains(soundEvent))
        {
            soundEvent.setPaused(false);
        }
    }

    public void StopAllEvents (bool fadeout)
    {
        for (int i = 0; i < eventList.Count; i++)
        {
            if (fadeout)
                eventList[i].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            else
                eventList[i].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void PauseAllEvents()
    {
        for (int i = 0; i < eventList.Count; i++)
        {
            eventList[i].setPaused(true);
        }
    }

    public void ResumeAllEvents()
    {
        for (int i = 0; i < eventList.Count; i++)
        {
            eventList[i].setPaused(false);
        }
    }

    public bool isPlaying(EventInstance soundEvent)
    {
        PLAYBACK_STATE state;
        soundEvent.getPlaybackState(out state);
        return !state.Equals(PLAYBACK_STATE.STOPPED);
    }


    // ************************ UTILS *******************************

    public static FMOD.ATTRIBUTES_3D To3DAttributes (Vector3 pos)
    {
        FMOD.ATTRIBUTES_3D attributes = new FMOD.ATTRIBUTES_3D();
        attributes.forward = ToFMODVector(Vector3.forward);
        attributes.up = ToFMODVector(Vector3.up);
        attributes.position = ToFMODVector(pos);

        return attributes;
    }

    public static FMOD.VECTOR ToFMODVector (Vector3 vec)
    {
        FMOD.VECTOR temp;
        temp.x = vec.x;
        temp.y = vec.y;
        temp.z = vec.z;

        return temp;

    }

}

class SoundManagerMovingSound
{
    Transform transform;
    EventInstance eventInst;

    public SoundManagerMovingSound(Transform transform, EventInstance eventInst)
    {
        this.transform = transform;
        this.eventInst = eventInst;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public EventInstance GetEventInstance()
    {
        return eventInst;
    }

}

public class SoundManagerParameter
{
    string name;
    float value;

    public SoundManagerParameter (string name, float value)
    {
        this.name = name;
        this.value = value;
    }

    public string GetName()
    {
        return name;
    }

    public float GetValue()
    {
        return value;
    }

}

/*
[SerializeField]
public class SoundBank
{
    public string xPath;
}
*/
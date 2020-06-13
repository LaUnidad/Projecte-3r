using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoMAnager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("LISTA DE TEXTOS")]

    public GameObject[] texts;
    [Header("Tiempo primer texto")]

    public float timetxt1;


    [Header("TIMER")]

    public float timer;

    public float timerToLive;


    public int numOfText;

    bool condition;

    private GameObject Player;

    bool TrapCondition1;
    bool TrapCondition2;

    [Header("CONDITIONS ORDER")]

    public int AbsrorbConditionOrder;
    
    
    void Start()
    {
        texts = GameObject.FindGameObjectsWithTag("TutoText");
        AllCaput();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindingCondition())
        {
            timer += 1*Time.deltaTime;
            if(TimeToRuntext() <= 0)
            {
                Debug.Log("NOP");
            }
            else
            {
                if(timer>= TimeToRuntext())
                {
                    RunText();
                    timerToLive += 1*Time.deltaTime;

                    if(timerToLive>= TimeToStayAlive())
                    {
                        ChangeText();
                    }
                }
            }
        }
        else
        {
            AllCaput();
        }
        Conditions();
        //Debug.Log(FindingCondition());


    }
    public bool FindingCondition()
    {
        foreach(GameObject obj in texts)
        {
            if(obj.GetComponent<SubtitlesScript>().Order == numOfText && obj.GetComponent<SubtitlesScript>().condition == true)
            {
                return true;
            }
        }
        return false;
    }
    public void ChangeText()
    {
        timerToLive = 0;
        timer = 0;
        foreach(GameObject obj in texts)
        {
            if(obj.GetComponent<SubtitlesScript>().Order == numOfText)
            {
                obj.SetActive(false);
            }
        }
        numOfText += 1;
    }

    public void RunText()
    {
        foreach(GameObject obj in texts)
        {
            if(obj.GetComponent<SubtitlesScript>().Order == numOfText)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
    public void AllCaput()
    {
        foreach(GameObject obj in texts)
        {
            obj.SetActive(false);
        }
    }

    public float TimeToRuntext()
    {
        foreach(GameObject obj in texts)
        {
            if(obj.GetComponent<SubtitlesScript>().Order == numOfText)
            {
                return obj.GetComponent<SubtitlesScript>().DelayTime;
            }
        }
        return 0;
    }

    public float TimeToStayAlive()
    {
        foreach(GameObject obj in texts)
        {
            if(obj.GetComponent<SubtitlesScript>().Order == numOfText)
            {
                return obj.GetComponent<SubtitlesScript>().TimeActived;
            }
        }
        return 0;
    }

    public void ConditionVAlidated()
    {
        foreach(GameObject obj in texts)
        {
            if(obj.GetComponent<SubtitlesScript>().Order == numOfText)
            {
                obj.GetComponent<SubtitlesScript>().condition = false;
            }
        }
    }

    public void Conditions()
    {
        if(Player.GetComponent<HippiCharacterController>().blackboard.Biomassa>0 && !TrapCondition1)
        {
            numOfText = AbsrorbConditionOrder;
            ConditionVAlidated();
            TrapCondition1 = true;
            //RunText();

        }
        if(Player.GetComponent<HippiCharacterController>().AfectedByTheGas && !TrapCondition2)
        {
            ConditionVAlidated();
            TrapCondition2 = true;
        }
    }
}

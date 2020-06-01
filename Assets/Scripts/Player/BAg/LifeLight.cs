using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLight : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesR;
    public Material DangerMat;
    public Material NormalMat;

    public float TimeToChange;

    GameObject Player;

    public float timer;

    public bool Flip;

    

    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        mesR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Timer();
        ChangingRate();
        Parpadeo();
    }
    public void Timer()
    {
        if(Player.GetComponent<HippiCharacterController>().blackboard.currentLife < 100)
        {
            timer += 1 * Time.deltaTime;

            if(timer >= TimeToChange)
            {
                if(Flip)
                {
                    Flip = false;
                    timer = 0;
                }
                else
                {
                    Flip = true;
                    timer = 0;
                }
            }
        }

    }
    public void Parpadeo()
    {
        if(Flip)
        {
            mesR.material = DangerMat;
        }
        else
        {
            mesR.material = NormalMat;
        }
        if(Player.GetComponent<HippiCharacterController>().blackboard.currentLife<= 0)
        {
            mesR.material = DangerMat;
        }
        if(Player.GetComponent<HippiCharacterController>().blackboard.currentLife>= 100)
        {
            mesR.material = NormalMat;
        }
    }   
    public void ChangingRate()
    {
       if(Player.GetComponent<HippiCharacterController>().blackboard.currentLife<= 80 && Player.GetComponent<HippiCharacterController>().blackboard.currentLife>60)
       {
           TimeToChange = 2f;
       }
       else if(Player.GetComponent<HippiCharacterController>().blackboard.currentLife<= 60 && Player.GetComponent<HippiCharacterController>().blackboard.currentLife>40)
       {
           TimeToChange = 1.5f;
       }
       else if(Player.GetComponent<HippiCharacterController>().blackboard.currentLife<= 40 && Player.GetComponent<HippiCharacterController>().blackboard.currentLife>20)
       {
           TimeToChange = 1f;
       }
       else if(Player.GetComponent<HippiCharacterController>().blackboard.currentLife<= 20 && Player.GetComponent<HippiCharacterController>().blackboard.currentLife>0)
       {
           TimeToChange = 0.5f;
       }
    }
}

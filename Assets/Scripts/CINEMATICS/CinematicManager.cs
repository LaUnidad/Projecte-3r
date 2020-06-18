using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject MainCamera;

    private GameObject Canvas;

    private GameObject Credits;

    private GameObject Player;
    [Header("LISTA DE CINEMATICAS")]

    public GameObject[] Cinematicas;

    int TrapCinematic;

    bool TrapInBool;
    void Start()
    {
        TrapCinematic = 10;
        Cinematicas = GameObject.FindGameObjectsWithTag("Cinematic");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Player = GameObject.FindGameObjectWithTag("Player");
        Credits = GameObject.FindGameObjectWithTag("Credits");
    }

    // Update is called once per frame
    void Update()
    {

        DesactivateTheOdersWhileRunningOne();
        
        if(IsAnyCinematicActive())
        {
            Debug.Log("EyeOfTheTiger");
            MainCamera.gameObject.SetActive(false);
            Canvas.gameObject.SetActive(false);
            Player.gameObject.SetActive(false);
        }
        else
        {
            ReturnToNormalMode();
        }
        
    }

    public bool IsAnyCinematicActive()
    {
        foreach(GameObject obj in Cinematicas)
        {
            if(obj.GetComponent<Cinamatic1>().ImActive)
            {
                return true;
            }
            else
            {
               return false;
            }
        }
        return false;
    }
    public void ReturnToNormalMode()
    {
        MainCamera.gameObject.SetActive(true);
        Canvas.gameObject.SetActive(true);
        Player.gameObject.SetActive(true);
        AnyoneIsActive();
        
    }

    public int WichCinematicIsRunning()
    {
        foreach(GameObject obj in Cinematicas)
        {
            if(obj.GetComponent<Cinamatic1>().ImActive)
            {
                return obj.GetComponent<Cinamatic1>().CinematicOrder;
            }
        }
        return TrapCinematic;
    }

    public void AnyoneIsActive()
    {
        foreach(GameObject obj in Cinematicas)
        {
            obj.GetComponent<Cinamatic1>().ImActive = false;
        }
    }

    public void DesactivateTheOdersWhileRunningOne()
    {
        foreach(GameObject obj in Cinematicas)
        {
           if(obj.GetComponent<Cinamatic1>().CinematicOrder != WichCinematicIsRunning())
           {
               obj.SetActive(false);
           }
           else if(obj.GetComponent<Cinamatic1>().CinematicOrder == 10)
           {
                obj.SetActive(false);
           }
        }
        /*
        if(!TrapInBool)
        {
            TrapCinematic = 10;
            TrapInBool = true;
        }
        */
        
    }

    
}

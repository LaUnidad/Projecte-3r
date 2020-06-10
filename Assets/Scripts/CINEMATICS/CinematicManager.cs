using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject MainCamera;

    private GameObject Canvas;
    [Header("LISTA DE CINEMATICAS")]

    public GameObject[] Cinematicas;

    int TrapCinematic;

    bool TrapInBool;
    void Start()
    {
        TrapCinematic = 0;
        Cinematicas = GameObject.FindGameObjectsWithTag("Cinematic");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

        DesactivateTheOdersWhileRunningOne();
        if(IsAnyCinematicActive())
        {
            MainCamera.gameObject.SetActive(false);
            Canvas.gameObject.SetActive(false);
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

        if(!TrapInBool)
        {
            TrapCinematic = 10;
            TrapInBool = true;
        }
        
    }

    
}

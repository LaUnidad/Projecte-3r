using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinamatic1 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject MainCamera;

    private GameObject Canvas;

    [Header("LISTA DE CAMARAS")]

    public GameObject[] CamerasC1;

    [Header("TAG DEL CONJUNTO DE CAMARAS")]
    public string TagOfCamera;

    [Header("ORDEN DE CINEMATICAS (DEL 0 AL ...)")]

    public int CinematicOrder;

    [Header("NI CASO")]



    int numOfCamera;
    float timer;

    public bool ImActive;

    
    
    
    void Start()
    {
        
        numOfCamera = 0;
        CamerasC1 = GameObject.FindGameObjectsWithTag(TagOfCamera);
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //MainCamera.gameObject.SetActive(false);
        //CamerasC1[numOfCamera].GetComponent<CAM_Movment>().Move = true;
        WichCameraHaveToMove(numOfCamera);
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        //Canvas.gameObject.SetActive(false);
        DesactivateCams();
        //ImActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        ImActive = true;
        TimeUntilChange();

       // Debug.Log(CamerasC1.Length + "   y el numofCamers es: "+ numOfCamera);
        
    }

    public void ChangeTheCamera()
    {
        timer = 0;
        //CamerasC1[numOfCamera].SetActive(false);
        foreach(GameObject obj in CamerasC1)
        {
            if(obj.GetComponent<CAM_Movment>().Order == numOfCamera)
            {
                obj.gameObject.SetActive(false);
                
            }
        }
        numOfCamera +=1;
        if(numOfCamera>= CamerasC1.Length)
        {
            ImActive = false;
            //ReturnToThePlayer();
            
        }
        else
        {
            //CamerasC1[numOfCamera].SetActive(true);
            //CamerasC1[numOfCamera].GetComponent<CAM_Movment>().Move = true;
            WichCameraHaveToMove(numOfCamera);
        }
        
    }
    public void TimeUntilChange()
    {
        
        timer+=1*Time.deltaTime;
        
        if(timer>= TimeToBeAlive())
        {
            ChangeTheCamera();
        }
    }
    public float TimeToBeAlive()
    {
        if(numOfCamera>= CamerasC1.Length)
        {
            return 0;
        }
        return CamerasC1[numOfCamera].GetComponent<CAM_Movment>().TimeToStayAlive;
    }

    public void ReturnToThePlayer()
    {
        //MainCamera.SetActive(true);   
        //Canvas.gameObject.SetActive(true);
    }

    public void DesactivateCams()
    {
        /*
        foreach(GameObject obj in CamerasC1)
        {
            if(obj != CamerasC1[0])
            {
                obj.SetActive(false);
            }
        }
        */
        foreach(GameObject obj in CamerasC1)
        {
            if(obj.GetComponent<CAM_Movment>().Order != numOfCamera)
            {
                obj.gameObject.SetActive(false);
                
            }
        }
    }

    public void WichCameraHaveToMove(float num)
    {
        foreach(GameObject obj in CamerasC1)
        {
            if(obj.GetComponent<CAM_Movment>().Order == num)
            {
                obj.gameObject.SetActive(true);
                obj.GetComponent<CAM_Movment>().Move = true;
            }
            else
            {
                Debug.Log("Numero de ORDEN incorrecto");
            }
        }
    }
}

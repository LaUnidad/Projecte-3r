using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinamatic1 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject MainCamera;

    private GameObject Canvas;

    public GameObject[] CamerasC1;

    int numOfCamera;
    float timer;
    
    
    void Start()
    {
        numOfCamera = 0;
        CamerasC1 = GameObject.FindGameObjectsWithTag("CameraC1");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamera.gameObject.SetActive(false);
        CamerasC1[numOfCamera].GetComponent<CAM_Movment>().Move = true;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        Canvas.gameObject.SetActive(false);
        DesactivateCams();
    }

    // Update is called once per frame
    void Update()
    {
        TimeUntilChange();
        Debug.Log(CamerasC1.Length + "   y el numofCamers es: "+ numOfCamera);
    }

    public void ChangeTheCamera()
    {
        timer = 0;
        CamerasC1[numOfCamera].SetActive(false);
        numOfCamera +=1;
        if(numOfCamera>= CamerasC1.Length)
        {
            ReturnToThePlayer();
        }
        else
        {
            CamerasC1[numOfCamera].SetActive(true);
            CamerasC1[numOfCamera].GetComponent<CAM_Movment>().Move = true;
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
        return CamerasC1[numOfCamera].GetComponent<CAM_Movment>().TimeToStayAlive;
    }

    public void ReturnToThePlayer()
    {
        MainCamera.SetActive(true);   
        Canvas.gameObject.SetActive(true);
    }

    public void DesactivateCams()
    {
        foreach(GameObject obj in CamerasC1)
        {
            if(obj != CamerasC1[0])
            {
                obj.SetActive(false);
            }
        }
    }


}

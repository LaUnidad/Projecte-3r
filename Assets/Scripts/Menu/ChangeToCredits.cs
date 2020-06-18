using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToCredits : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Van;

    public float TimeForChange;

    public GameObject LastCam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Van.GetComponent<TimeToSayGoodBye>().final)
        {
            Invoke("ChangeScene",TimeForChange);
        }
    }

    public void ChangeScene()
    {
        //SceneManager.LoadScene("Credits");
        LastCam.GetComponent<CAM_Movment>().RotationVelocity = 0;
    }
}

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

    public GameObject credits;

    public bool Doit;
    void Start()
    {
        //credits.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Van.GetComponent<TimeToSayGoodBye>().final || Doit)
        {
            Invoke("ChangeScene", TimeForChange);
        }
    }

    public void ChangeScene()
    {
        SoundManager.Instance.StopAllEvents(true);
        SceneManager.LoadScene("Credits");
        //GameManager.Instance.earthquake.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //LastCam.GetComponent<CAM_Movment>().RotationVelocity = 0;
        //credits.GetComponent<Credits>().credits = true;
        //Invoke("GoToMainMenu", 44);
        
    }

    public void GoToMainMenu()
    {
        

        SceneManager.LoadScene("MainMenuGold");
    }
}

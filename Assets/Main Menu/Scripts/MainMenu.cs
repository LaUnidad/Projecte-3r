using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float time = 6f;
    public Animator anim;
    public Camera cam;

    public GameObject OptionsFirstButton, MenuFirstSelected, ToogleFullScreen;
    public GameObject m_Options;

    public GameObject volumeImage, fullScreenImage, menufirst, exitbutton, Start_Exit;

    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;

    public string Accept = "event:/FX/Menu/Accept";
    public string Button = "event:/FX/Menu/Button";
    public string Cancel = "event:/FX/Menu/Cancel";
    public string Exit = "event:/FX/Menu/Exit";
    public string PauseSound = "event:/FX/Menu/Pause";
    public string Resume = "event:/FX/Menu/Resume";

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetButtonDown("B"))
        {
            m_Options.SetActive(false);
            Back();
        }

        if (EventSystem.current.currentSelectedGameObject == OptionsFirstButton)
        {
            volumeImage.GetComponent<Animation>().Play("SoundAnim");
        }
        else volumeImage.GetComponent<Animation>().Stop();

        if (EventSystem.current.currentSelectedGameObject == ToogleFullScreen)
        {
            fullScreenImage.GetComponent<Animation>().Play("FullScreenAnim");
        }
        else fullScreenImage.GetComponent<Animation>().Stop();

        if (EventSystem.current.currentSelectedGameObject == menufirst)
        {
            menufirst.GetComponent<Animation>().Play("Play");
        }
        else menufirst.GetComponent<Animation>().Stop();

        if (EventSystem.current.currentSelectedGameObject == exitbutton)
        {
            exitbutton.GetComponent<Animation>().Play("Exit");
        }
        else exitbutton.GetComponent<Animation>().Stop();
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        anim.SetTrigger("start");
        SoundResume();
        StartCoroutine(LoadMainScene());
    }

    public void Options()
    {
        cam.GetComponent<CameraMenu>().ChangeView(1);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionsFirstButton);
        //EventSystem.current.currentInputModule

    }

    public void Back()
    {
        cam.GetComponent<CameraMenu>().ChangeView(0);
        SetActiveAllChildren(this.transform);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MenuFirstSelected);
        Start_Exit.SetActive(true);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void QuitGame()
    {
        SoundExit();
        StartCoroutine(QuitGameCo());
        Application.Quit();
    }

    IEnumerator QuitGameCo()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(1);

    }

    public void SayTes()
    {

    }

    public void SetActiveAllChildren(Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);

            SetActiveAllChildren(child);
        }
    }

    public void SetUnActiveAllChildren(Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            SetActiveAllChildren(child);
        }
    }

    public void SoundAccept()
    {
        SoundManager.Instance.PlayOneShotSound(Accept, transform);
    }

    public void SoundResume()
    {
        SoundManager.Instance.PlayOneShotSound(Resume, transform);
    }

    public void SoundExit()
    {
        SoundManager.Instance.PlayOneShotSound(Exit, transform);
    }


}

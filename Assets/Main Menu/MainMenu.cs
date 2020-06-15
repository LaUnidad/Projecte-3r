using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float time = 10f;
    public Animator anim;
    public Camera cam;

    public GameObject OptionsFirstButton, MenuFirstSelected, ToogleFullScreen;
    public GameObject m_Options;

    public GameObject volumeImage, fullScreenImage;

    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
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
            volumeImage.GetComponent<Animation>().Play("Volume");
        }
        else volumeImage.GetComponent<Animation>().Stop();

        if (EventSystem.current.currentSelectedGameObject == ToogleFullScreen)
        {
            fullScreenImage.GetComponent<Animation>().Play("FullScreen");
        }
        else fullScreenImage.GetComponent<Animation>().Stop();
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        anim.SetTrigger("start");
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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("Loading");

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


}

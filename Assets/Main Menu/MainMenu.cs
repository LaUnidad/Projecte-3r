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

    public GameObject OptionsFirstButton, MenuFirstSelected;
    public GameObject m_Options;

    private void Update()
    {
        if (Input.GetButtonDown("B"))
        {
            m_Options.SetActive(false);
            Back();
        }
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

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("ProvesTerrain");

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

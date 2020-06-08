using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float time = 10f;
    public Animator anim;
    public Camera cam;

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        anim.SetTrigger("start");
        StartCoroutine(LoadMainScene());
    }

    public void Options()
    {
        cam.GetComponent<CameraMenu>().ChangeView(1);
    }

    public void Back()
    {
        cam.GetComponent<CameraMenu>().ChangeView(0);
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


}

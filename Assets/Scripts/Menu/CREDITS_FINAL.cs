using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CREDITS_FINAL : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cam1;

    public GameObject cam2;
    public GameObject n;
    public GameObject Credits;
    void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("cam2GOOOO", 43);
    }

    public void cam2GOOOO()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        Credits.GetComponent<Credits>().credits = true;
        Invoke("ChangeScene", 110);
    }

    public void ChangeScene()
    {

        n.GetComponent<CreditsManager>().StopSong();
        SceneManager.LoadScene(0);
    }
}

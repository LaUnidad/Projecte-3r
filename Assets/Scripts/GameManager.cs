using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestartGameElement
{
    void RestartGame();
}

public class GameManager : MonoBehaviour
{
    public GameObject m_player;

    HippiCharacterController cc;

    private static GameManager _instance;

    List<IRestartGameElement> m_RestartGameElements = new List<IRestartGameElement>();
    bool m_GameActive = true;


    public bool isPaused;


    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = FindObjectOfType<HippiCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Pause();

        if (cc.currentHealth <= 0) m_GameActive = false;
    }

    public void RestartGame()
    {
        foreach (IRestartGameElement l_RestartGameElement in m_RestartGameElements)
            l_RestartGameElement.RestartGame();
        m_GameActive = true;
    }
    public void AddRestartGameElement(IRestartGameElement RestartGameElement)
    {
        m_RestartGameElements.Add(RestartGameElement);
    }

    public void Pause()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        if (isPaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
}

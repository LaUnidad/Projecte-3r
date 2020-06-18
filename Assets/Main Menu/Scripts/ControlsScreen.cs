using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadAsyncOperation()
    {
        
        yield return new WaitForSeconds(6f);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
        new WaitForEndOfFrame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //start async operation
        StartCoroutine(LoadAsyncOperation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadAsyncOperation()
    {
            yield return new WaitForSeconds(13.5f);
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync(0);    
    }
}

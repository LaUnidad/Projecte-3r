using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private bool m_CanDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        m_CanDestroy = true;
    }

    private void OnBecameInvisible()
    {
        if (m_CanDestroy)
        {
            StartCoroutine(WaitToDestroy());
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}

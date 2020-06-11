using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manta_Listener : MonoBehaviour
{

    public GameObject m_MantaFlock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_MantaFlock.SetActive(true);
            StartCoroutine(WaitAndDestroy(5f));
        }
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(m_MantaFlock);
        Destroy(gameObject);
    }
}

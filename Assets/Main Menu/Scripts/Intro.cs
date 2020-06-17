using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{

    public GameObject g1, g2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroCo());
    }


    IEnumerator IntroCo()
    {
        yield return new WaitForSeconds(15f);
        Destroy(g1);
        Destroy(g2);
    }
}

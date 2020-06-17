using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMenu : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Update()
    {
        transform.LookAt(target);
    }

    
}

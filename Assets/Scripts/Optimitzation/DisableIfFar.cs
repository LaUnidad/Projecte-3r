using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfFar : MonoBehaviour
{
    private GameObject itemActivatorObject;
    private ObjectActivator activationScript;

    // Start is called before the first frame update
    void Start()
    {
        itemActivatorObject = GameObject.Find("ItemActivatorObject");
        activationScript = itemActivatorObject.GetComponent<ObjectActivator>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.01f);

        activationScript.m_activatorItems.Add(new ActivatorItem { item = this.gameObject, itemPos = transform.position });
    }

    
}

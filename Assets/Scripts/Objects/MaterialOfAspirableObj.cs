using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOfAspirableObj : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesR;
    public Material AspirableObjMaterial;
    void Start()
    {
        mesR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "AspirableObject")
        {
            mesR.material = AspirableObjMaterial;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColor : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Area1Obj;
    MeshRenderer meshR;

    public Material Death;

    public bool AllDead;

    public float TotalObj;
    void Start()
    {
        Area1Obj = GameObject.FindGameObjectsWithTag("Tronco");
        meshR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsEveryoneDeath());
        ChangeColor();
    }
    public void ChangeColor()
    {
        if(IsEveryoneDeath())
        {
            meshR.material = Death;
        }
    }

    public bool IsEveryoneDeath()
    { 
        foreach(GameObject obj in Area1Obj)
        {
            if(obj.GetComponent<TreeColor>().ImDeath == false)
            {
                AllDead = false;
            }
            else
            {
                AllDead = true;
            } 
        }   
        return AllDead;
    }

}

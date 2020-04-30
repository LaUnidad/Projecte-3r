using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColor : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Area1Obj;
    MeshRenderer meshR;
    public float NumOfDeads;
    public Material Death;

    void Start()
    {
        Area1Obj = GameObject.FindGameObjectsWithTag("Tronco");
        meshR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        Debug.Log(EveryoneDeath());
    }
    public void ChangeColor()
    {
       if(NumOfDeads >= Area1Obj.Length)
       {
            meshR.material = Death;
       }
      
       
        
    }
    public float EveryoneDeath()
    {
        foreach(GameObject obj in Area1Obj)
        {
            if(obj.GetComponent<TreeColor>().ImDeath == true && obj.GetComponent<TreeColor>().OnList == false)
            {
                NumOfDeads += 1;
                obj.GetComponent<TreeColor>().OnList = true;
            }
        }
        return NumOfDeads;    
    }
   

}

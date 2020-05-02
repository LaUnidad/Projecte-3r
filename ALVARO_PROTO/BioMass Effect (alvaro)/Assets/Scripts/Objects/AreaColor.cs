using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColor : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Area1Obj;
    
    public float NumOfDeads;
    
    public bool KillZone;

    void Start()
    {
        Area1Obj = GameObject.FindGameObjectsWithTag("Tronco");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        Debug.Log("I KILL ->"+SumDeaths()+ "  of " + Area1Obj.Length);
    }
    public void ChangeColor()
    {
       if(NumOfDeads >= Area1Obj.Length)
       {
            KillZone = true;
       }   
    }
    public float SumDeaths()
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

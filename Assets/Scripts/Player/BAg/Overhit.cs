using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class Overhit : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public float NumOfPowerToChangeColor;

    public Material ChargeMat;
    public Material LowMat;

    MeshRenderer meshR;

    EventInstance Warning;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meshR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }
    
    public bool HaveToChange()
    {
        if(player.GetComponent<HippiCharacterController>().blackboard.Power <= NumOfPowerToChangeColor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeColor()
    {
        if(HaveToChange())
        {
            meshR.material = LowMat;


        }
        else
        {
            meshR.material = ChargeMat;
        }
    }
    
}

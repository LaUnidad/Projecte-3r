using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBiomasIntaciate : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;

    public bool rotate;
    public bool stop;
    public float RotationVelocity;

    float timer;

    float timer2;

    public float TimeToRotate;

    //public bool active;

    public GameObject BiommasObj;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       ActiveRotation();
        if(rotate)
        { 
            //active = false;
            transform.RotateAround(Player.transform.position, Vector3.up, RotationVelocity * Time.deltaTime);
            InstanciateBiomass();
        }
        StopRotate();
    }
    public void StopRotate()
    {
        if(rotate)
        {
            timer += 1*Time.deltaTime;

            if(timer>= TimeToRotate)
            {
                rotate = false;
                timer = 0;
                timer2 = 0;
            }
        }
    }
    public void ActiveRotation()
    {
        /*
        if(active)
        {
            timer = 0;
            timer2 = 0;
            //rotate = true;
        }
        */
    }
    public void InstanciateBiomass()
    {
        timer2 += 1* Time.deltaTime;
        if(timer2>= 0.05)
        {
            Instantiate(BiommasObj, this.transform.position,this.transform.rotation);
            timer2 = 0;
        }   
        
    }
}

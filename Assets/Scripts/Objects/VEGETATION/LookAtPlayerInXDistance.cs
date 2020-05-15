using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerInXDistance : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    GameObject TreeMaster;

    public float DistanceToDetect;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
       
        if(DistanceWithPlayer() <= DistanceToDetect && DistanceWithPlayer() >= 1)
        {
            this.transform.LookAt(Player.transform.position,Vector3.up);
        }
        
    }
    public float DistanceWithPlayer()
    {
        return Vector3.Distance(Player.transform.position, this.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLACKBOARD_AspirableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float ForceToAbsorb;
    public float SpeedToAbsorb;

    public GameObject Player;

    public GameObject Gun;

    public float Biomass;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gun = GameObject.FindGameObjectWithTag("Gun");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

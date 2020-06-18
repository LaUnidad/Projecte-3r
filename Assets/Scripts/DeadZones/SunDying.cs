using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDying : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    float posy;

    public float velocity;

    public bool Move;

    public GameObject rotationPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        posy = this.transform.position.y;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Move = player.GetComponent<HippiCharacterController>().blackboard.RoketMan;

        if(Move)
        {
            this.transform.RotateAround(rotationPoint.transform.position, Vector3.forward, velocity*Time.deltaTime);
        }
    }
}

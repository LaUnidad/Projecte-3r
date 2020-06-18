using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingIntensity : MonoBehaviour
{
    // Start is called before the first frame update

    public Light d_Light;

    private GameObject player;
    public bool Change;

    public float velocity;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Change = player.GetComponent<HippiCharacterController>().blackboard.RoketMan;

        if(Change)
        {
            d_Light.intensity -= velocity * Time.deltaTime;
        }
    }
}

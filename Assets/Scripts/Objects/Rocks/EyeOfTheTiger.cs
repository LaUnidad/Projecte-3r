using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOfTheTiger : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(Player.transform.position,Vector3.up);
    }
}

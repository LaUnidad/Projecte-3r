using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer m_MR;
    public Material mat1;
    public Material mat2;
    public Material mat3;

     GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");  
        m_MR = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeMaterial();
    }

    public void ChangeMaterial()
    {
        if(Player.GetComponent<HippiCharacterController>().blackboard.Power <= 10)
        {
            m_MR.material = mat3;
        }
        if(Player.GetComponent<HippiCharacterController>().blackboard.Power <= 50 && Player.GetComponent<HippiCharacterController>().blackboard.Power >= 10)
        {
            m_MR.material = mat2;
        }
        if(Player.GetComponent<HippiCharacterController>().blackboard.Power >= 100)
        {
            m_MR.material = mat1;
        }
    }
}

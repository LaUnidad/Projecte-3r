using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] DeadElements;

    GameObject CurrentObject;

    public float timer;

    public float TimeToKill;

    GameObject Player;

    void Start()
    {
        DeadElements = GameObject.FindGameObjectsWithTag("DeadController");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<HippiCharacterController>().AfectedByTheGas)
        {
            if(DeadElements.Length == 0)
            {
                Debug.Log("No hay vegetacion que eliminar! Hasta luego, Saludos;)))");
            }
            else
            {
                KillElementsInTime();
            }
        }
    }

    public int RandomElement()
    { 
        return Random.Range(0, DeadElements.Length-1);
    }

    public GameObject SelectElement(int random)
    {
        CurrentObject = DeadElements[random];
        
        if(CurrentObject.GetComponent<DeathTree>().Death)
        {
            SelectElement(RandomElement());
        }
        return CurrentObject;
    }
    public void KillThatElement(GameObject wichElement)
    {
        wichElement.GetComponent<DeathTree>().KillFruits = true;
    }

    public void KillElementsInTime()
    {
        timer += 1*Time.deltaTime;

        if(timer>= TimeToKill)
        {
            KillThatElement(SelectElement(RandomElement()));
            timer = 0;
        }
    }
}

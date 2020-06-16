using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesScript : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("TIEMPO ACTIVADO")]
    public float TimeActived;

    [Header("POSICION EN LA COLA")]
    public int Order;

    [Header("SI NO SE ACTIVA POR CONDICION, ESTE ES EL TIEMPO DE ESPERA")]

    public float DelayTime;

    [Header("MARCALO SI NECESITA UNA CONDICION")]

    public bool condition;

    
    void Start()
    {
        
    }
   
    void Update()
    {
       
    }
}

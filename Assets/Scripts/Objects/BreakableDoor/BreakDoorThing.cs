using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class BreakDoorThing : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ActivateExplosion;

    private Rigidbody rgbd;
    private MeshRenderer mesR;

    public float timer;

    [Range (0,10)]public float VelocityToDisapear;
    void Start()
    {
        mesR = GetComponent<MeshRenderer>();
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MikelBay();
    }
    public void MikelBay()
    {
        if(ActivateExplosion)
        {
            rgbd.isKinematic = false;
            rgbd.useGravity = true;
            DisolveMe();
            //Invoke("TimeToGo", 4);
        }
    }
    public void TimeToGo()
    {
        Destroy(this.gameObject);
    }
    public void DisolveMe()
    {
        mesR.material.SetFloat("VelocityToDisapear", timer);

        timer += 1* Time.deltaTime * (VelocityToDisapear/10);  
        if(timer>= 1)
        {
                Destroy(this.gameObject);
        }  
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class BreakDoorThing : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ActivateExplosion;

    private Rigidbody rgbd;
    void Start()
    {
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
            Invoke("TimeToGo", 8);
        }
    }
    public void TimeToGo()
    {
        Destroy(this.gameObject);
    }
}

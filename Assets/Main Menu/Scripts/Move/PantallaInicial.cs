using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaInicial : MonoBehaviour {

    public Camera cam;
    
    

	public void Play()
    {
        cam.GetComponent<CameraMenu>().ChangeView(1);
    }

    public void Options()
    {
        cam.GetComponent<CameraMenu>().ChangeView(2);
    }

    public void Quit()
    {

    }

    public void Back()
    {
        cam.GetComponent<CameraMenu>().ChangeView(0);
    }
}

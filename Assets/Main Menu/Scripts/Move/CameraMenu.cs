using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour {

    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;

	// Use this for initialization
	void Start () {

        currentView = views[0];


    }

    void Update() {

        if (Input.GetKeyDown (KeyCode.Q))
        {
            currentView = views[0];
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            currentView = views[1];
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentView = views[2];
        }

    }

    public void ChangeView (int n)
    {
        currentView = views[n];
    }

    // Update is called once per frame
    void LateUpdate() {

        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        transform.eulerAngles = currentAngle;

    }   
}

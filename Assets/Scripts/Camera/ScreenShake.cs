using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && !isShaking)
        {
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;
            isShaking = true;
            StartShake(1f, .5f);
        }
    }

    void LateUpdate()
    {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1, 1) * shakePower;
            float yAmount = Random.Range(-1, 1) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

           // shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

        }
        
        if (shakeTimeRemaining <= 0 && isShaking)
        {
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            isShaking = false;
        }
        
    }

    public void StartShake(float length, float power)
    {
        //originalPos = transform.position;
       // originalRot = transform.rotation;
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;
    }
}

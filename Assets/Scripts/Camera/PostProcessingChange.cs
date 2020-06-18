using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PostProcessingChange : MonoBehaviour
{
    Volume volume;

    private ColorAdjustments colorAdj;
    public Vector4 colorVector;

    public float colorChange;
    public float changeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.sharedProfile.TryGet(out colorAdj);
    }

    // Update is called once per frame
    void Update()
    {
        colorChange -= (changeSpeed * Time.deltaTime);
        colorChange = Mathf.Clamp(colorChange, 0.4f, 1);

        colorVector = new Vector4(colorChange, 1, colorChange, 0f);

        colorAdj.colorFilter.SetValue(new NoInterpColorParameter(colorVector, true));
    }
}

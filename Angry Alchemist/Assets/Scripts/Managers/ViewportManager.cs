using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportManager : MonoBehaviour
{
    public static ViewportManager instance;

    Transform cameraPivot;
    Camera cam;

    float noisePos;
    Vector3 noise;
    [Range(0f,1f)]
    public float noiseIntensity;
    [Range(0f, 5f)]
    public float noiseMult;

    public AnimationCurve intensityCurve, multCurve, timeCurve;

    public float timeShake, currentTime;

    public bool useCurves, screenShaking;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Start()
    {
        // get reference to the camera
        cam = Camera.main;
        // get reference to the camera's pivot
        cameraPivot = cam.transform.parent;


        noisePos = 0;

    }

    private void Update()
    {

        //if(Input.GetMouseButtonDown(0))
        //{
        //    ShakeScreen(0.4f);
        //}

        //if (useCurves)
        //{
        //    noiseIntensity = intensityCurve.Evaluate(timeCurve.Evaluate(Time.time));
        //    noiseMult = multCurve.Evaluate(timeCurve.Evaluate(Time.time));
        //}



        if(screenShaking)
        {


            var temp = currentTime / timeShake;

            noiseIntensity = intensityCurve.Evaluate(temp);
            noiseMult = multCurve.Evaluate(temp);


            noise = new Vector3((Mathf.PerlinNoise(noisePos, 0f) * noiseMult) - (noiseMult * 0.5f), 0, (Mathf.PerlinNoise(0f, noisePos) * noiseMult) - (noiseMult * 0.5f));

            // have the cam offset the pivot
            cam.transform.position = cameraPivot.position + noise;
            // bring the camera back
            cam.transform.position += Vector3.up * 10f;

            // increase the noise over time
            noisePos += noiseIntensity;


            currentTime -= Time.deltaTime;

            if(currentTime <= 0f)
            {
                screenShaking = false;
                noiseIntensity = 0f;
                noiseMult = 0f;
            }

        }




    }



    public void ShakeScreen(float time)
    {
        screenShaking = true;
        timeShake = time;
        currentTime = timeShake;
    }

}

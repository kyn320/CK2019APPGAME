using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;

public class BgSkyAniControl : MonoBehaviour
{
    public Camera camera;
    public ColorGrading cg;
    public bool isWorking = true;
    public float workingTime;
    public float workingSpeed;
    public float workingTemperature;

    private void Awake()
    {
        camera.GetComponent<PostProcessVolume>().profile.TryGetSettings(out cg);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking && !GameManager.Instance.isBegin) {
            if (workingTime >= GameManager.Instance.GetPlayTime())
            {
                cg.temperature.value += workingSpeed * Time.deltaTime;
                if(cg.temperature.value > workingTemperature)
                {
                    cg.temperature.value = 27.0f;
                    isWorking = false;
                }
            }
        }
    }
}

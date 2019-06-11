using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;

public class BgSkyAniControl : MonoBehaviour
{
    public Camera camera;
    public ColorGrading cg;
    public int currentWorking = 0;
    public const int workingCnt = 2;
    [Header("플레이 남은 시간(분)")]
    public float[] workingTime;
    public float[] workingSpeed;
    public float[] workingTemperature;

    private void Awake()
    {
        camera.GetComponent<PostProcessVolume>().profile.TryGetSettings(out cg);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWorking < workingCnt) {
            if (workingTime[currentWorking] >= GameManager.Instance.GetPlayTime())
            {
                cg.temperature.value += workingSpeed[currentWorking] * Time.deltaTime;
                if(cg.temperature.value > workingTemperature[currentWorking])
                {
                    cg.temperature.value = workingTemperature[currentWorking];
                    currentWorking++;
                }
            }
        }
    }
}

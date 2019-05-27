using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSkyAniControl : MonoBehaviour
{
    public bool isWorking = true;
    public float workingTime;
    public float workingSpeed;
    public float workingPosY = -60.0f;

    // Update is called once per frame
    void Update()
    {
        if (isWorking && !GameManager.Instance.isBegin) {
            if (workingTime >= GameManager.Instance.GetPlayTime())
            {
                transform.position += Vector3.up * workingSpeed * Time.deltaTime;
                if(transform.position.y > workingPosY)
                {
                    Vector3 v = transform.position;
                    v.y = workingPosY;
                    transform.position = v;
                    isWorking = false;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXUIPlayer : SFXPlayer
{
    
    public override void Play()
    {
        //if (Camera.main == null)
        //    return;

        soundManager.PlayBGS(eventPath, Camera.main.gameObject, volume);

        StartCoroutine(UpdateLifeTime());
    }

    public override void Play(string eventPath)
    {
        if (Camera.main == null)
            return;

        soundManager.PlayBGS(eventPath, Camera.main.gameObject, volume);

        StartCoroutine(UpdateLifeTime());
    }

    


}

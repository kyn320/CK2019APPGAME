using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXUIPlayer : SFXPlayer
{
    
   
    public override void Play()
    {
        soundManager.PlayBGS(eventPath, Camera.main.gameObject, volume);

        StartCoroutine(UpdateLifeTime());
    }

    public override void Play(string eventPath)
    {
        soundManager.PlayBGS(eventPath, Camera.main.gameObject, volume);

        StartCoroutine(UpdateLifeTime());
    }

    


}

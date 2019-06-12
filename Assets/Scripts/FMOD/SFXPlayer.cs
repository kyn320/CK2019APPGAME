using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SoundManager soundManager;

    public float volume = 0.3f;
    public int parameter;

    public bool useLifeTime;
    
    public bool randomLifeTime;
    public float lifeTime;
    public float minLifeTime, maxLifeTime;
    float currentLifeTime;

    [FMODUnity.EventRef]
    public string eventPath;

    public virtual void Play()
    {
        soundManager.PlayBGS(eventPath, gameObject, volume, parameter);

        StartCoroutine(UpdateLifeTime());
    }

    public virtual void Play(string eventPath)
    {
        soundManager.PlayBGS(eventPath, gameObject, volume, parameter);

        StartCoroutine(UpdateLifeTime());
    }

    protected IEnumerator UpdateLifeTime()
    {
        while (useLifeTime)
        {
            if (currentLifeTime <= 0)
            {
                if (randomLifeTime)
                {
                    currentLifeTime = Random.Range(minLifeTime, maxLifeTime);
                }
                else
                    currentLifeTime = lifeTime;
            }
            else
                currentLifeTime -= Time.deltaTime;

            yield return null;
        }
    }


}

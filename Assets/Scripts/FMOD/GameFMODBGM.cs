using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFMODBGM : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPath;
    public FMOD.Studio.EventInstance medieval;

    private void Awake()
    {
        medieval = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
        medieval.setVolume(0.03f);
    }

    // Update is called once per frame
    void Update()
    {
        FMOD.Studio.PLAYBACK_STATE soundState;
        medieval.getPlaybackState(out soundState);
        if (soundState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            medieval.start();
        }
    }
}

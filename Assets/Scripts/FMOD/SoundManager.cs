using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SoundManager : Singleton<SoundManager>
{

    public EventInstance medievalBGM;
    public ParameterInstance locationBGM;

    public EventInstance medievalBGS;
    public ParameterInstance locationBGS;

    public bool muteBGM;
    public bool muteBGS;

    protected override void Awake()
    {
        base.Awake();

        SFXPlayer.soundManager = BGMPlayer.soundManager = this;

        muteBGM = PlayerPrefs.GetInt("muteBGM", 0) == 0 ? false : true;
        muteBGS = PlayerPrefs.GetInt("muteBGS", 0) == 0 ? false : true;
    }

    public void MuteBGM(bool work) {
        muteBGM = work;
        PlayerPrefs.SetInt("muteBGM",work == true ? 1 : 0);

        if (work)
        {
            medievalBGM.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else {
            medievalBGM.start();
        }
    }

    public void MuteBGS(bool work) {
        muteBGS = work;
        PlayerPrefs.SetInt("muteBGS", work == true ? 1 : 0);

        if (work)
        {
            medievalBGS.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void PlayBGM(string eventPath, GameObject g, float volume, int parameter = 0)
    {
        if (muteBGM)
            return;

        StopBGM();

        medievalBGM = RuntimeManager.CreateInstance(eventPath);
        medievalBGM.getParameter("intensity", out locationBGM);
        medievalBGM.set3DAttributes(RuntimeUtils.To3DAttributes(g));
        //medievalBGM.setVolume(volume);
        locationBGM.setValue(parameter);

        medievalBGM.start();
    }

    public void ChangeParameter(int parameter) {
        medievalBGM.getParameter("intensity", out locationBGM);
        locationBGM.setValue(parameter);
    }

    public void StopBGM()
    {
        print("stop");
        medievalBGM.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        medievalBGM.release();
    }

    public void PlayBGS(string eventPath, GameObject g, float volume,int parameter = 0)
    {
        if (muteBGS)
            return;

        medievalBGS = RuntimeManager.CreateInstance(eventPath);
        medievalBGS.getParameter("Parameter", out locationBGS);
        medievalBGS.set3DAttributes(RuntimeUtils.To3DAttributes(g));
        //medievalBGS.setVolume(volume);
        locationBGS.setValue(parameter);
        medievalBGS.start();
    }


}

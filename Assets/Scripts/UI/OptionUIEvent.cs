using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionUIEvent : MonoBehaviour
{
    public TextMeshProUGUI bgmToggleText;
    public TextMeshProUGUI bgsToggleText;

    private void Start()
    {
        bool mute = SoundManager.Instance.muteBGM;
        bgmToggleText.text = (mute == true ? "OFF" : "ON");

        mute = SoundManager.Instance.muteBGS;
        bgsToggleText.text = (mute == true ? "OFF" : "ON");
    }

    public void OnToggleBGM()
    {
        bool mute = SoundManager.Instance.muteBGM;
        bgmToggleText.text = (mute == true ? "ON" : "OFF");
        SoundManager.Instance.MuteBGM(!mute);
    }

    public void OnToggleBGS()
    {
        bool mute = SoundManager.Instance.muteBGS;
        bgsToggleText.text = (mute == true ? "ON" : "OFF");
        SoundManager.Instance.MuteBGS(!mute);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoUIEvent : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlayer;

    bool played = false;

    private void Update()
    {
        if (videoPlayer.isPrepared)
            played = true;

        if (played && !videoPlayer.isPlaying) {
            OnSkip();
        }
    }

    public void OnSkip()
    {

        SceneManager.LoadScene("MainScene");

    }

}

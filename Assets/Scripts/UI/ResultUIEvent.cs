using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUIEvent : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MatchingScene");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}

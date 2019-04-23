using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIEvent : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MatchingScene");
    }
}

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

    public void GoToInfo() {
        SceneManager.LoadScene("InfoScene");
    }

    public void OnViewOption() {
        //TODO :: 옵션 창 키기
    }

}

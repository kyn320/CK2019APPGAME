using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameUITimer : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int time = (int)GameManager.instance.gameTime;
        text.text = "  " + (time / 60).ToString() + " : " + (time % 60 / 10).ToString() + (time % 60 % 10).ToString();
    }
}

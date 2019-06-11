using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GameUITimer : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int time = (int)GameManager.Instance.GetPlayTime();
        text.text = "  " + (time / 60).ToString() + " : " + (time % 60 / 10).ToString() + (time % 60 % 10).ToString();
    }
}

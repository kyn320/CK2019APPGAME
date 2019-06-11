using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GameUIStartCount : MonoBehaviour
{
    float count;
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        count = 3.9f;
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        text.text = ((int)count).ToString();
        if((int)count == 0)
        {
            text.text = "START";
        }
        if(count <= 0.0f)
        {
            GameManager.Instance.isBegin = false;
            GameManager.Instance.Init();
            gameObject.SetActive(false);
        }
    }
}

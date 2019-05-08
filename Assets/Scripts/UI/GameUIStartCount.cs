using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameUIStartCount : MonoBehaviour
{
    float count;
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        count = 3.9f;
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        text.text = ((int)count).ToString();
        if(count <= 0.0f)
        {
            GameManager.Instance.isBegin = false;
            gameObject.SetActive(false);
        }
    }
}

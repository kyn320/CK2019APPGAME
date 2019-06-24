using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GameUIStartCount : MonoBehaviour
{
    int tmpCount;
    float count;
    TextMeshProUGUI text;

    [FMODUnity.EventRef]
    public string countPath;
    [FMODUnity.EventRef]
    public string startPath;
    public FMOD.Studio.EventInstance count_medieval;
    public FMOD.Studio.EventInstance start_medieval;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        count = 3.9f;
        tmpCount = 0;


        count_medieval = FMODUnity.RuntimeManager.CreateInstance(countPath);
        count_medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
        start_medieval = FMODUnity.RuntimeManager.CreateInstance(startPath);
        start_medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
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
        else
        {
            if((int)count != tmpCount)
            {
                tmpCount = (int)count;
                count_medieval.start();
            }
        }
        if(count <= 0.0f)
        {
            GameManager.Instance.isBegin = false;
            GameManager.Instance.Init();
            gameObject.SetActive(false);
            start_medieval.start();
        }
    }
}

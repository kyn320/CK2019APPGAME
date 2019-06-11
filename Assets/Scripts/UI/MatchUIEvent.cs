using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchUIEvent : MonoBehaviour
{

    public Sprite[] tipSprites;

    public Image tipImage;

    public Image matchingCircle;

    float fillAmount = 0f;

    private void Start()
    {
        tipImage.sprite = tipSprites[Random.Range(0, tipSprites.Length)];
    }


    private void Update()
    {
        fillAmount = Mathf.Abs(Mathf.Sin(Time.time));

        matchingCircle.fillAmount = fillAmount;

        if (fillAmount >= 0.99f)
            matchingCircle.fillClockwise = false;

        if (fillAmount <= 0.01f)
            matchingCircle.fillClockwise = true;

    }

}

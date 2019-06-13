using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;

public class GameUIScore : MonoBehaviour
{
    public Image[] scores = new Image[8];
    public Image goldScore;

    public Sprite localSprite;
    public Sprite otherSprite;
    public Sprite nonSprite;

    public Sprite localGoldSprite;
    public Sprite otherGoldSprite;

    int localIndex = 0;
    int otherIndex = 7;

    private void Start()
    {
        if(PhotonNetwork.LocalPlayer.CustomProperties["Type"].Equals("hera"))
        {
            Sprite tmpSprite = localSprite;
            localSprite = otherSprite;
            otherSprite = tmpSprite;

            tmpSprite = localGoldSprite;
            localGoldSprite = otherGoldSprite;
            otherGoldSprite = tmpSprite;
        }
    }

    public void UpdateScore()
    {
        foreach (Image img in scores)
        {
            img.sprite = otherGoldSprite;
        }

        int buttonCount = 0;
        int otherCount = 0;

        foreach (ButtonManager button in GameManager.Instance.buttons)
        {
            if (button.occupationTarget == GameManager.Instance.localPlayer)
            {
                buttonCount++;
            }
            else if (button.occupationTarget != null)
            {
                otherCount++;
            }
        }

        for(int i = 0; i < buttonCount; i++)
        {
            scores[localIndex + i].sprite = localSprite;
        }
        for(int i = 0; i < otherCount; i++)
        {
            scores[otherIndex - i].sprite = localSprite;
        }
    }

    public void UpdateScore(ButtonManager changeButton, bool nonTarget)
    {
        if (changeButton.isHidden)
        {
            if(changeButton.occupationTarget == GameManager.Instance.localPlayer)
            {
                goldScore.sprite = localGoldSprite;
            }
            else
            {
                goldScore.sprite = otherGoldSprite;
            }
        }
        else
        {
            if(changeButton.occupationTarget == GameManager.Instance.localPlayer)
            {
                if (!nonTarget)
                {
                    otherIndex++;
                    scores[otherIndex].sprite = nonSprite;
                }
                scores[localIndex].sprite = localSprite;
                localIndex++;
            }
            else
            {
                if (!nonTarget)
                {
                    localIndex--;
                    scores[localIndex].sprite = nonSprite;
                }
                scores[otherIndex].sprite = otherSprite;
                otherIndex--;
            }
        }
    }
}

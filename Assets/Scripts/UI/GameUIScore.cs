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
                scores[localIndex].sprite = localSprite;
                localIndex++;
                if (!nonTarget)
                {
                    otherIndex--;
                    scores[otherIndex].sprite = nonSprite;
                }
            }
            else
            {
                scores[otherIndex].sprite = otherSprite;
                otherIndex--;
                if(!nonTarget)
                {
                    localIndex--;
                    scores[localIndex].sprite = nonSprite;
                }
            }
        }
    }
}

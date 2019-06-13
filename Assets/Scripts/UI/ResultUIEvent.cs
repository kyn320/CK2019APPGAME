using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResultUIEvent : MonoBehaviour
{
    /*
     제우스 = #7bbada
     헤라 = #ff8484 
     */

    const string heraColorHex = "#FF8484";
    const string zeusColorHex = "#7bbada";

    public GameObject winTitle, loseTitle;

    public TextMeshProUGUI buttonCountText;

    public GameObject winEffect;
    public Image winBackgroundImage;

    public GameObject loseEffect;

    public Image illustSlot;

    public Sprite[] heraSprites, zeusSprites;

    public BGMPlayer bgm;
    public SFXPlayer sfx;

    [FMODUnity.EventRef]
    public string heraBgm, zeusBgm;
    [FMODUnity.EventRef]
    public string heraSfx, zeusSfx;

    private void Start()
    {
        ViewResult();
    }

    public void ViewResult()
    {

        int characterType = PlayerPrefs.GetInt("CharacterType", 0);
        int GameResult = PlayerPrefs.GetInt("GameResult", 0);
        int buttonCount = PlayerPrefs.GetInt("ButtonCount", 0);
        
        if (characterType == 0)
            GoToMain();

        if (GameResult == 1)
        {
            winTitle.SetActive(true);
            winEffect.SetActive(true);
            Color color = new Color();
            switch (characterType)
            {
                case 1:
                    ColorUtility.TryParseHtmlString(zeusColorHex, out color);
                    illustSlot.sprite = zeusSprites[GameResult];
                    bgm.eventPath = zeusBgm;
                    sfx.eventPath = zeusSfx;
                    break;
                case 2:
                    ColorUtility.TryParseHtmlString(heraColorHex, out color);
                    illustSlot.sprite = heraSprites[GameResult];
                    bgm.eventPath = heraBgm;
                    sfx.eventPath = heraSfx;
                    break;
            }
            illustSlot.SetNativeSize();
            winBackgroundImage.color = color;
        }
        else
        {
            switch (characterType)
            {
                case 1:
                    illustSlot.sprite = zeusSprites[GameResult];
                    bgm.eventPath = zeusBgm;
                    break;
                case 2:
                    illustSlot.sprite = heraSprites[GameResult];
                    bgm.eventPath = heraBgm;
                    break;
            }
            sfx.useLifeTime = false;

            illustSlot.SetNativeSize();
            loseTitle.SetActive(true);
            loseEffect.SetActive(true);
        }

        buttonCountText.text = buttonCount.ToString();

    }

    public void GameStart()
    {
        SceneManager.LoadScene("MatchingScene");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }



}

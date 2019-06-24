using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class GameUICharacterSlot : MonoBehaviour
{
    public bool isLocal = true;

    const string heraColorHex = "#FF8484";
    const string zeusColorHex = "#7bbada";

    public Image illustSlot;
    public Sprite zeusillust;
    public Sprite heraillust;

    public Image nameSlot;
    public Sprite zeusNameSlot;
    public Sprite heraNameSlot;
    public TextMeshProUGUI nameText;

    public Image RushIcon;
    public Image RollIcon;
    public Image MoveIcon;

    // Start is called before the first frame update
    void Start()
    {
        Color color = new Color();
        int characterType;
        if (PhotonNetwork.OfflineMode) characterType = (isLocal)? 1 : 2;
        else
            characterType = ((PhotonNetwork.LocalPlayer.CustomProperties["Type"].Equals("zeus")) == isLocal)? 1 : 2;
        switch (characterType)
        {
            case 1:
                ColorUtility.TryParseHtmlString(zeusColorHex, out color);
                illustSlot.sprite = zeusillust;
                nameSlot.sprite = zeusNameSlot;
                nameText.text = "Zeus";
                break;
            case 2:
                ColorUtility.TryParseHtmlString(heraColorHex, out color);
                illustSlot.sprite = heraillust;
                nameSlot.sprite = heraNameSlot;
                nameText.text = "Hera";
                break;
        }
        GetComponent<Image>().color = color;
    }

    public void UpdateBuffStat(UnitStatCode statCode, Stat stat)
    {
        int value = (int)(stat.add / stat.normal * 100);
        if (statCode == UnitStatCode.ROLL_RESISTANCE) value = (int)stat.add;
        if(value < 25)
        {
            value = 0;
        }
        else if(value < 50)
        {
            value = 1;
        }
        else if(value < 100)
        {
            value = 2;
        }
        else if(value == 100)
        {
            value = 3;
        }
        switch (statCode)
        {
            case UnitStatCode.MOVE_SPEED:
                MoveIcon.sprite = Resources.Load<Sprite>("Icon/spr_icon_move0" + value.ToString());
                break;
            case UnitStatCode.RUSH_POWER:
                RushIcon.sprite = Resources.Load<Sprite>("Icon/spr_icon_rush0" + value.ToString());
                break;
            case UnitStatCode.ROLL_RESISTANCE:
                RollIcon.sprite = Resources.Load<Sprite>("Icon/spr_icon_roll0" + value.ToString());
                break;
        }

        Debug.Log("Icon/spr_icon_move0" + value.ToString());
    }
}

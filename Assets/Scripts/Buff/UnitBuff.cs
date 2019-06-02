using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

[System.Serializable]
public class UnitBuff : MonoBehaviourPunCallbacks
{
    UnitManager target = null;
    [SerializeField]
    UnitStatCode statCode;
    [SerializeField]
    float addValue;

    [FMODUnity.EventRef]
    public string eventPath;
    public FMOD.Studio.EventInstance medieval;

    private void Awake()
    {
        medieval = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            do
            {
                statCode = (UnitStatCode)Random.Range(0, (int)UnitStatCode.SIZE);
            } while (statCode == UnitStatCode.JUMP_POWER);
            addValue = GameManager.Instance.standardStat[statCode] * Random.Range(0, 3) * 0.01f;
            GameManager.Instance.buffText.text += GetComponent<ButtonManager>().photonView.ToString() + " : [" + statCode + "] " + addValue.ToString() + "\n";
            photonView.RPC("BuffSet", RpcTarget.Others, statCode, addValue);
        }
    }

    [PunRPC]
    public void BuffSet(UnitStatCode statCode, float addValue)
    {
        this.statCode = statCode;
        this.addValue = addValue;
        GameManager.Instance.buffText.text += GetComponent<ButtonManager>().photonView.ToString() + " : [" + statCode + "] " + addValue.ToString() + "\n";
    }

    public void Set(UnitManager unit)
    {
        if(target != null && target != unit)
        {
            End();
        }
        target = unit;

        Begin();
    }

    public void Begin()
    {
        target.stat.AddStat(statCode, addValue);
        medieval.start();
    }

    public void End()
    {
        target.stat.AddStat(statCode, -addValue);
    }

    public void isHidden()
    {
        addValue = GameManager.Instance.standardStat[statCode] * 0.2f;
        if(statCode == UnitStatCode.ROLL_RESISTANCE && Random.Range(0, 3) == 0)
        {
            addValue = 100.0f;
        }
    }
}

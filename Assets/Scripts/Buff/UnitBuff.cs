using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

[System.Serializable]
public class BuffData
{
    public float[] addValue = new float[4];
}

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
            int code = 0;
            do
            {
                code = Random.Range(0, (int)UnitStatCode.SIZE * 3);
                //statCode = (UnitStatCode)Random.Range(0, (int)UnitStatCode.SIZE);
            } while (GameManager.Instance.buffCheckFlag[code]);
            GameManager.Instance.buffCheckFlag[code] = true;
            statCode = (UnitStatCode)(code / 3);
            addValue = GameManager.Instance.standardStat[statCode] * GameManager.Instance.buffStatValue[(int)statCode].addValue[code % 3] * 0.01f;
            photonView.RPC("BuffSet", RpcTarget.Others, statCode, addValue);
        }
    }

    [PunRPC]
    public void BuffSet(UnitStatCode statCode, float addValue)
    {
        this.statCode = statCode;
        this.addValue = addValue;
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
        addValue = GameManager.Instance.standardStat[statCode] * GameManager.Instance.buffStatValue[(int)statCode].addValue[3] * 0.01f;
        if(statCode == UnitStatCode.ROLL_RESISTANCE && Random.Range(0, 3) == 0)
        {
            addValue = 100.0f;
        }
    }
}

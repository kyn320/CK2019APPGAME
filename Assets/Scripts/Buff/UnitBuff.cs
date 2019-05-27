using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitBuff : MonoBehaviour
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
        do
        {
            statCode = (UnitStatCode)Random.Range(0, (int)UnitStatCode.SIZE);
        } while (statCode == UnitStatCode.JUMP_POWER);
        addValue = GameManager.Instance.standardStat[statCode] * (1 + Random.Range(0, 3) * 2) * 0.01f;
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

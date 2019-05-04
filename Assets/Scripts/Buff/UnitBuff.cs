using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitBuff : MonoBehaviour
{
    UnitManager target = null;
    Stat addStat;
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

    public void Set(UnitManager unit)
    {
        if(target != null && target != unit)
        {
            End();
        }
        unit.stat.GetStat(statCode, ref addStat);

        Begin();
    }

    public void Begin()
    {
        addStat.add += addValue;
        medieval.start();
    }

    public void End()
    {
        addStat.add -= addValue;
    }
}

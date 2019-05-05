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
}

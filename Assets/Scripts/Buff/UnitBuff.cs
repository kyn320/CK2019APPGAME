using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitBuff : MonoBehaviour
{
    UnitStat unitStat = null;
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
    }

    public void Set(UnitStat stat)
    {
        if(unitStat != null && unitStat != stat)
        {
            End();
        }
        unitStat = stat;
        unitStat.GetStat(statCode, addStat);

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

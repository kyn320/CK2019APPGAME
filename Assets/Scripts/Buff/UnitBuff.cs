using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitBuff
{
    UnitStat unitStat = null;
    Stat addStat;
    [SerializeField]
    UnitStatCode statCode;
    [SerializeField]
    float addValue;

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
    }

    public void End()
    {
        addStat.add -= addValue;
    }
}

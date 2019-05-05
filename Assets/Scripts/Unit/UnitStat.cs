using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stat
{
    [SerializeField]
    private float max;
    [SerializeField]
    private float normal;
    public float add;

    public float Value
    {
        get
        {
            return (normal + add > max) ? max : normal + add;
        }
    }
}

public enum UnitStatCode
{
    MOVE_SPEED,
    RUSH_POWER,
    JUMP_POWER,
    ROLL_RESISTANCE
}

public class UnitStat : MonoBehaviour
{
    public UnitManager manager;

    public Stat moveSpeed;
    public Stat rushPower;
    public Stat jumpPower;
    public Stat rollResistance;

    private void Awake()
    {
        manager = GetComponent<UnitManager>();
    }

    public bool AddStat(UnitStatCode code, float addValue)
    {
        bool isDone = true;
        switch (code)
        {
            case UnitStatCode.MOVE_SPEED:
                moveSpeed.add += addValue;
                break;
            case UnitStatCode.JUMP_POWER:
                jumpPower.add += addValue;
                break;
            case UnitStatCode.RUSH_POWER:
                rushPower.add += addValue;
                break;
            case UnitStatCode.ROLL_RESISTANCE:
                rollResistance.add += addValue;
                break;
            default:
                isDone = false;
                break;
        }

        return isDone;
    }
}

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

    public bool GetStat(UnitStatCode code, ref Stat stat)
    {
        bool isDone = true;
        switch (code)
        {
            case UnitStatCode.MOVE_SPEED:
                stat = moveSpeed;
                break;
            case UnitStatCode.JUMP_POWER:
                stat = jumpPower;
                break;
            case UnitStatCode.RUSH_POWER:
                stat = rushPower;
                break;
            case UnitStatCode.ROLL_RESISTANCE:
                stat = rollResistance;
                break;
            default:
                isDone = false;
                break;
        }

        return isDone;
    }
}

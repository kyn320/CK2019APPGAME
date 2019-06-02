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

    public void SetValue(float normal, float max)
    {
        this.normal = normal;
        this.max = max;
    }

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
    MOVE_SPEED = 0,
    RUSH_POWER,
    ROLL_RESISTANCE,
    SIZE,
    JUMP_POWER = 101,
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

    private void Start()
    {
        Dictionary<UnitStatCode, float> stat = GameManager.Instance.standardStat;
        moveSpeed.SetValue(stat[UnitStatCode.MOVE_SPEED], stat[UnitStatCode.MOVE_SPEED] * 1.5f);
        rushPower.SetValue(stat[UnitStatCode.RUSH_POWER], stat[UnitStatCode.RUSH_POWER] * 2.0f);
        jumpPower.SetValue(stat[UnitStatCode.JUMP_POWER], stat[UnitStatCode.JUMP_POWER]);
        rollResistance.SetValue(0.0f, 100.0f);
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

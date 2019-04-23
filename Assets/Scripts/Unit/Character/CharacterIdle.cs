using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : UnitIdle
{
    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.MOVE);
        linkStates.Add(UnitStateCode.JUMP);
        linkStates.Add(UnitStateCode.RUSH);
        linkStates.Add(UnitStateCode.ROLL);
        linkStates.Add(UnitStateCode.FALL);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Update()
    {
        
    }
}

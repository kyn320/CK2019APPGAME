﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFall : UnitFall
{
    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
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
        if(transform.position.y < -50.0f)
        {
            transform.position = manager.spawnPosition;
            manager.rigidbody.velocity = Vector3.zero;
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

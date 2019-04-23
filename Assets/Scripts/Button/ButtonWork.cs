﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWork : ButtonState
{
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
        Vector3 vector = transform.position;
        vector.y -= manager.occupationDepth / manager.occupationTime * Time.deltaTime;

        if(vector.y <= -manager.occupationDepth + manager.offsetY)
        {
            vector.y = manager.offsetY;
            manager.SetState(ButtonStateCode.FINISH);
            manager.meshRenderer.material = manager.target.meshRenderer.material;
            manager.occupationTarget = manager.target;
        }
        transform.position = vector;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("GroundChecker"))
        {
            manager.SetState(ButtonStateCode.IDLE);
        }
    }
}

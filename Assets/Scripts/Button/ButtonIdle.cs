using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIdle : ButtonState
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
        vector.y += manager.occupationDepth / manager.occupationTime * Time.deltaTime;

        if (vector.y > manager.offsetY)
        {
            vector.y = manager.offsetY;
        }
        transform.position = vector;
    }
}


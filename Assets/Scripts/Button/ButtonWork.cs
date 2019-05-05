using System.Collections;
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
            //OccupationSetting();
            manager.SetState(ButtonStateCode.FINISH);
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

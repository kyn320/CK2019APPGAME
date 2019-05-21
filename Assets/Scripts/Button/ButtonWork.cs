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


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (manager.currentState != ButtonStateCode.WORK) return;
    //    if (collision.collider.name.Equals("GroundChecker"))
    //    {
    //        manager.SetState(ButtonStateCode.IDLE);
    //    }
    //}
    //
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (manager.currentState != ButtonStateCode.WORK) return;
    //    if (collision.collider.name.Equals("GroundChecker"))
    //    {
    //        manager.SetState(ButtonStateCode.IDLE);
    //    }
    //}
}

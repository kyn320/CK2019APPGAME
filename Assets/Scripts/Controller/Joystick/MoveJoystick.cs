using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : Joystick
{
    public override void OnJoystickEnter()
    {
        base.OnJoystickEnter();
    }

    public override void OnJoystickStay()
    {
        base.OnJoystickStay();
        if(GetTarget().currentState == UnitStateCode.IDLE)
        {
            GetTarget().SetState(UnitStateCode.MOVE);
        }
        Vector3 vector = stick.transform.localPosition;
        Vector3 dir = vector.normalized;
        dir.z = dir.y;
        dir.y = 0.0f;
        GetTarget().ctrlMoveDir = dir;

        GetTarget().ctrlMoveDistance = vector.sqrMagnitude / (joystickRadius * joystickRadius);
    }

    public override void OnJoystickExit()
    {
        base.OnJoystickExit();
        GetTarget().ctrlMoveDir = Vector3.zero;
        GetTarget().ctrlMoveDistance = 0.0f;

        if (GetTarget().currentState == UnitStateCode.MOVE)
        {
            GetTarget().SetState(UnitStateCode.IDLE);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionJoystick : Joystick
{
    public override void OnJoystickEnter()
    {
        base.OnJoystickEnter();
    }

    public override void OnJoystickStay()
    {
        base.OnJoystickStay();
    }

    public override void OnJoystickExit()
    {
        Vector3 vector = stick.transform.localPosition;
        if(vector.sqrMagnitude / (joystickRadius * joystickRadius) < 0.3f)
        {
            GetTarget().SetState(UnitStateCode.JUMP);
        }
        else
        {
            Vector3 dir = vector.normalized;
            dir.z = dir.y;
            dir.y = 0.0f;
            GetTarget().ctrlRushDir = dir;
            GetTarget().ctrlRushDistance = vector.sqrMagnitude / (joystickRadius * joystickRadius);
            GetTarget().SetState(UnitStateCode.RUSH);
        }

        base.OnJoystickExit();
    }
}

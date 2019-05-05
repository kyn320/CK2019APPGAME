using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : Joystick
{
    private void Update()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir.z = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            dir.z = -1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = 1;
        }

        GetTarget().ctrlMoveDir = dir;
        if (dir.sqrMagnitude > 0.0f)
        {
            GetTarget().ctrlMoveDistance = 1f;
            GetTarget().SetState(UnitStateCode.MOVE);
        }
        else
        {
            GetTarget().ctrlMoveDistance = 0f;
            if (GetTarget().currentState == UnitStateCode.MOVE)
                GetTarget().SetState(UnitStateCode.IDLE);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            GetTarget().SetState(UnitStateCode.JUMP);
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            float rad = GetTarget().transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            GetTarget().ctrlRushDir = new Vector3(Mathf.Sin(rad), 0.0f, Mathf.Cos(rad));
            GetTarget().ctrlRushDistance = 1f;
            GetTarget().SetState(UnitStateCode.RUSH);
        }
    }

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

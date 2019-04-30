using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : UnitMove
{
    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
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

        //dir.z = dir.y;
        //dir.y = 0.0f;
        //
        Quaternion rotation = transform.rotation;
        int t = (manager.ctrlMoveDir.z > 0) ? 1 : -1;
        float angle = rotation.eulerAngles.y + manager.ctrlMoveDir.x * t;
        Vector3 moveDir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0.0f, Mathf.Cos(Mathf.Deg2Rad * angle));
        rotation = Quaternion.LookRotation(moveDir);
        transform.rotation = rotation;

        //rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, rotation, rotateSpeed * Time.deltaTime);
        Vector3 moveVelocity = manager.stat.moveSpeed.Value * moveDir * manager.ctrlMoveDir.z;
        moveVelocity.y = manager.rigidbody.velocity.y;

        manager.rigidbody.velocity = moveVelocity;
    }
}

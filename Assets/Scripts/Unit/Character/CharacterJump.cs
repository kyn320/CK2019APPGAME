using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : UnitJump
{
    [SerializeField]
    private Transform groundChecker;

    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
        linkStates.Add(UnitStateCode.ROLL);
        linkStates.Add(UnitStateCode.FALL);
    }

    public override void Enter()
    {
        base.Enter();
        manager.rigidbody.AddForce(Vector3.up * manager.jumpPower, ForceMode.Impulse);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void FixedUpdate()
    {
        Vector3 moveVelocity = manager.moveSpeed * manager.ctrlMoveDir * manager.ctrlMoveDistance;
        moveVelocity.y = manager.rigidbody.velocity.y;

        manager.rigidbody.velocity = moveVelocity;
        if (manager.rigidbody.velocity.y < 0.0f)
        {

            Collider[] cols = Physics.OverlapSphere(groundChecker.position, 0.25f, LayerMask.GetMask("Ground"));
            if (cols.Length > 0)
            {
                manager.SetState(UnitStateCode.IDLE);
            }
        }
    }
}

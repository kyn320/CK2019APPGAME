using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : UnitJump
{
    [SerializeField]
    private Transform groundChecker;

    [FMODUnity.EventRef]
    public string eventPath;
    public FMOD.Studio.EventInstance medieval;

    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
        linkStates.Add(UnitStateCode.ROLL);
        linkStates.Add(UnitStateCode.FALL);

        medieval = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
    }

    public override void Enter()
    {
        base.Enter();
        manager.rigidbody.AddForce(Vector3.up * manager.stat.jumpPower.Value, ForceMode.Impulse);
        medieval.start();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void FixedUpdate()
    {
        int dir = (manager.spawnRotation.y > 100.0f)? -1 : 1;
        Vector3 moveDir = new Vector3(manager.ctrlMoveDir.x, 0.0f, manager.ctrlMoveDir.z) * dir;
        transform.rotation = Quaternion.LookRotation(moveDir);
        Vector3 moveVelocity = manager.stat.moveSpeed.Value * moveDir * manager.ctrlMoveDistance;
        moveVelocity.y = manager.rigidbody.velocity.y;

        manager.rigidbody.velocity = moveVelocity;
        if (manager.rigidbody.velocity.y <= 0.0f)
        {

            Collider[] cols = Physics.OverlapSphere(groundChecker.position, 0.25f, LayerMask.GetMask("Ground"));
            if (cols.Length > 0)
            {
                manager.SetState(UnitStateCode.IDLE);
            }
        }
    }
}

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
        float rad = manager.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Sin(rad), 0.0f, Mathf.Cos(rad));
        Vector3 moveVelocity = manager.stat.moveSpeed.Value * dir * manager.ctrlMoveDistance;
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

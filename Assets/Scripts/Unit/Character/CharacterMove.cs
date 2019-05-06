using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : UnitMove
{
    [FMODUnity.EventRef]
    public string eventPath;
    public FMOD.Studio.EventInstance medieval;

    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
        linkStates.Add(UnitStateCode.JUMP);
        linkStates.Add(UnitStateCode.RUSH);
        linkStates.Add(UnitStateCode.ROLL);
        linkStates.Add(UnitStateCode.FALL);

        medieval = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
    }

    public override void Enter()
    {
        base.Enter();
        medieval.start();
    }

    public override void Exit()
    {
        base.Exit();
        medieval.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void Update()
    {
        FMOD.Studio.PLAYBACK_STATE soundState;
        medieval.getPlaybackState(out soundState);
        if (soundState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            medieval.start();
        }
        //dir.z = dir.y;
        //dir.y = 0.0f;
        //
        Quaternion rotation = transform.rotation;
        int t = (manager.ctrlMoveDir.z >= 0) ? 1 : -1;
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

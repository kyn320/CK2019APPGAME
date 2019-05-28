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

        int dir = (manager.spawnRotation.y > 100.0f)? -1 : 1;
        Vector3 moveDir = new Vector3(manager.ctrlMoveDir.x, 0.0f,manager.ctrlMoveDir.z) * dir;
        transform.rotation = Quaternion.LookRotation(moveDir);

        Vector3 moveVelocity = manager.stat.moveSpeed.Value * moveDir * manager.ctrlMoveDistance;
        moveVelocity.y = manager.rigidbody.velocity.y;

        manager.rigidbody.velocity = moveVelocity;
    }
}

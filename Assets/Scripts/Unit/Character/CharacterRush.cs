using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRush : UnitRush
{
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

        manager.rigidbody.velocity = manager.ctrlRushDir * manager.ctrlRushDistance * manager.stat.rushPower.Value;

        medieval.start();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Update()
    {

        if (!manager.animator.GetCurrentAnimatorStateInfo(0).IsName("RUSH"))
            return;

        float animationTime = manager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (animationTime >= 1.0f)
        {
            Debug.Log(manager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

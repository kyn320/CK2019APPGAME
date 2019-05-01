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
    }

    public override void Enter()
    {
        base.Enter();
        
        manager.rigidbody.AddForce(manager.ctrlRushDir * manager.ctrlRushDistance * manager.stat.rushPower.Value, ForceMode.Impulse);

        Vector3 v = manager.ctrlRushDir;
        v.y = -1.0f;

        medieval.start();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Update()
    {
        if(manager.rigidbody.velocity.sqrMagnitude <= 5.0f)
        {
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

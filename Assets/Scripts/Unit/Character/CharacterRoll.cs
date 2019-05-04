using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoll : UnitRoll
{
    [FMODUnity.EventRef]
    public string eventPath;
    public FMOD.Studio.EventInstance medieval;

    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.rigidbody.velocity.sqrMagnitude <= 0.05f)
        {
            Vector3 v = transform.rotation.eulerAngles;
            v.x = 0.0f;
            transform.rotation = Quaternion.Euler(v);
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

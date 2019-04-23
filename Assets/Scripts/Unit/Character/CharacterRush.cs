using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRush : UnitRush
{
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
        
        manager.rigidbody.AddForce(manager.ctrlRushDir * manager.ctrlRushDistance * manager.rushPower, ForceMode.Impulse);

        Vector3 v = manager.ctrlRushDir;
        v.y = -1.0f;
        Quaternion rotation = Quaternion.LookRotation(v);
        transform.rotation = rotation;
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Update()
    {
        if(manager.rigidbody.velocity.sqrMagnitude <= 5.0f)
        {
            Vector3 v = transform.rotation.eulerAngles;
            v.x = 0.0f;
            transform.rotation = Quaternion.Euler(v);
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoll : UnitRoll
{
    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
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

    // Update is called once per frame
    void Update()
    {
        if (manager.rigidbody.velocity.sqrMagnitude <= 5.0f)
        {
            Vector3 v = transform.rotation.eulerAngles;
            v.x = 0.0f;
            transform.rotation = Quaternion.Euler(v);
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

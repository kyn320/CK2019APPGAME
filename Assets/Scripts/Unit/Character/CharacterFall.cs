using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFall : UnitFall
{
    [SerializeField]
    private Transform groundChecker;

    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void FixedUpdate()
    {
        if(transform.position.y < -50.0f)
        {
            transform.position = manager.spawnPosition;
            manager.rigidbody.velocity = Vector3.zero;
        }
        Collider[] cols = Physics.OverlapSphere(groundChecker.position, 0.25f, LayerMask.GetMask("Ground"));
        if (cols.Length > 0)
        {
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

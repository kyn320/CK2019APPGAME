using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFall : UnitFall
{
    [SerializeField]
    private Transform groundChecker;

    private Collider collider;
    private Collider groundCollider;

    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);

        collider = GetComponent<Collider>();
        groundCollider = groundChecker.GetComponent<Collider>();
    }

    public override void Enter()
    {
        base.Enter();
        groundCollider.material = collider.material;
    }

    public override void Exit()
    {
        base.Exit();
        groundCollider.material = null;
    }

    private void FixedUpdate()
    {
        if(transform.position.y < -50.0f)
        {
            manager.Respawn(Vector3.zero);
        }
        Collider[] cols = Physics.OverlapSphere(groundChecker.position, 0.25f, LayerMask.GetMask("Ground"));
        if (cols.Length > 0)
        {
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

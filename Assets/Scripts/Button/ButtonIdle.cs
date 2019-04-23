using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIdle : ButtonState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Update()
    {
        Vector3 vector = transform.position;
        vector.y += manager.occupationDepth / manager.occupationTime * Time.deltaTime;

        if (vector.y > manager.offsetY)
        {
            vector.y = manager.offsetY;
        }
        transform.position = vector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //manager.SetState(ButtonStateCode.WORK);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("GroundChecker"))
        {
            manager.target = other.GetComponentInParent<UnitManager>();
            manager.SetState(ButtonStateCode.WORK);
        }
    }
}

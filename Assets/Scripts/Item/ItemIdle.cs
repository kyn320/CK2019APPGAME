using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdle : ItemState
{
    public override void Enter()
    {
        base.Enter();
        manager.GetComponent<ObjectAniControl>().enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        manager.GetComponent<ObjectAniControl>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (manager.currentState != ItemStateCode.IDLE) return;
        if (other.gameObject.CompareTag("Player"))
        {
            manager.target = other.GetComponentInParent<UnitManager>();
            if (manager.target.haveItem) return;

            manager.SetState(ItemStateCode.PICK);
        }
    }
}

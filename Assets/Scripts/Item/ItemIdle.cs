using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdle : ItemState
{
    public override void Exit()
    {
        base.Exit();

        manager.transform.parent = manager.target.transform;
        manager.target.haveItem = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.target = other.GetComponentInParent<UnitManager>();

            manager.SetState(ItemStateCode.PICK);
        }
    }
}

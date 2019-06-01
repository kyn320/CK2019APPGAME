﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdle : ItemState
{
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

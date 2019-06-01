﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : ItemState
{
    [Header("픽업 높이")]
    public float pickHeight = 2.5f;

    public float lerpTime = 1.0f;

    public override void Enter()
    {
        base.Enter();

        manager.transform.parent = manager.target.transform;
        manager.target.haveItem = true;
    }

    // Update is called once per frame
    void Update()
    {
        manager.transform.localPosition = Vector3.Lerp(manager.transform.localPosition, Vector3.up * pickHeight, lerpTime * Time.deltaTime);
    }
}

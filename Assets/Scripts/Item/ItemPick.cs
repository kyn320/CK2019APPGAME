using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : ItemState
{
    [Header("픽업 높이")]
    public float pickHeight = 2.5f;

    public float lerpTime = 1.0f;

    private bool isHold = false;

    public override void Enter()
    {
        base.Enter();

        manager.target.haveItem = manager;
        isHold = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHold)
        {
            manager.transform.localScale = Vector3.Lerp(manager.transform.localScale, Vector3.one * 0.5f, lerpTime * Time.deltaTime);
            manager.transform.position = Vector3.Lerp(manager.transform.position, manager.target.transform.position + Vector3.up * pickHeight, lerpTime * Time.deltaTime);

            if((manager.transform.position - manager.target.transform.position - Vector3.up * pickHeight).sqrMagnitude < 0.001f)
            {
                manager.transform.localScale = Vector3.one * 0.5f;
                isHold = true;
            }
        }
        else
        {
            manager.transform.position = manager.target.transform.position + Vector3.up * pickHeight;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFinish : ButtonState
{
    public override void Enter()
    {
        base.Enter();
        OccupationSetting();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Update()
    {
        Vector3 vector = transform.position;
        vector.y += manager.occupationDepth / 0.5f * Time.deltaTime;

        if(vector.y > manager.offsetY)
        {
            vector.y = manager.offsetY;
            manager.SetState(ButtonStateCode.IDLE);
        }
        transform.position = vector;
    }

    void OccupationSetting()
    {
        if (manager.occupationTarget && manager.target.haveItem)
        {
            manager.target.GetComponentInChildren<ItemManager>().SetState(ItemStateCode.REMOVE);
        }
        manager.meshRenderer.material = manager.target.skinnedMeshRenderer.material;
        manager.occupationTarget = manager.target;
        manager.buff.Set(manager.occupationTarget);
    }
}

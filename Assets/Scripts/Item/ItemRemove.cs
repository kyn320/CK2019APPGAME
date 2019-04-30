using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemove : ItemState
{
    float timer;

    public override void Enter()
    {
        base.Enter();

        gameObject.SetActive(false);
        timer = 0.0f;

        if (manager.target)
        {
            manager.target.haveItem = false;
            manager.target = null;
        }

        manager.transform.parent = null;
    }

    public override void Exit()
    {
        base.Exit();

        transform.position = new Vector3(0.0f, 1.0f, 0.0f);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 3.0f)
        {
            manager.SetState(ItemStateCode.IDLE);
        }
    }
}

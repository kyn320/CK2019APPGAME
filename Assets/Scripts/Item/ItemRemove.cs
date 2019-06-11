using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemove : ItemState
{
    float timer;

    public override void Enter()
    {
        base.Enter();

        manager.GetComponentInChildren<MeshRenderer>().enabled = false;
        //gameObject.SetActive(false);
        timer = 0.0f;

        if (manager.target)
        {
            manager.target.haveItem = null;
            manager.target = null;
        }

        manager.transform.parent = null;
    }

    public override void Exit()
    {
        base.Exit();

        transform.position = manager.spawnPosition;
        manager.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= manager.respawnTimer)
        {
            manager.SetState(ItemStateCode.DROP);
        }
    }
}

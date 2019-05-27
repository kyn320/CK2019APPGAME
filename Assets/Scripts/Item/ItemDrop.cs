using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : ItemState
{
    public Vector2 dir;
    public float jumpPower;

    public override void Enter()
    {
        base.Enter();

        jumpPower = Random.Range(2.0f, 4.0f);
        dir = new Vector2(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        jumpPower -= 9.8f * 9.8f * Time.deltaTime * 0.5f;
        manager.transform.position += new Vector3(dir.x, jumpPower, dir.y) * Time.deltaTime;

        if(manager.transform.position.y < 1.5f)
        {
            Vector3 v = manager.transform.position;
            v.y = 1.5f;
            manager.transform.position = v;
            manager.SetState(ItemStateCode.IDLE);
        }
    }
}

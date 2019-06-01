using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : ItemState
{
    public Vector3 dir;
    public float jumpPower;

    public override void Enter()
    {
        base.Enter();

        manager.transform.parent = null;

        //jumpPower = Random.Range(2.0f, 4.0f);
        //dir = new Vector2(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
        jumpPower = 8.0f;
        dir = manager.target.rigidbody.velocity;

        if (manager.target)
        {
            manager.target.haveItem = false;
            manager.target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        jumpPower -= 9.8f * 9.8f * Time.deltaTime * 0.5f;
        manager.transform.position += new Vector3(dir.x, jumpPower, dir.z) * Time.deltaTime;

        if(jumpPower < 0.0f)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, LayerMask.GetMask("Ground")))
            {
                manager.SetState(ItemStateCode.IDLE);
            }
            if (transform.position.y < -50.0f)
            {
                manager.SetState(ItemStateCode.REMOVE);
            }
        }

        //if(manager.transform.position.y < 1.5f)
        //{
        //    Vector3 v = manager.transform.position;
        //    v.y = 1.5f;
        //    manager.transform.position = v;
        //    manager.SetState(ItemStateCode.IDLE);
        //}
    }
}

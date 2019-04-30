using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour
{
    public ItemManager manager;

    public virtual void Awake()
    {
        manager = GetComponent<ItemManager>();
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }
}

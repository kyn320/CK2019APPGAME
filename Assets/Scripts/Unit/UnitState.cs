using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    public UnitManager manager;
    public List<UnitStateCode> linkStates = new List<UnitStateCode>();

    public virtual void Awake()
    {
        manager = GetComponent<UnitManager>();
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }
}

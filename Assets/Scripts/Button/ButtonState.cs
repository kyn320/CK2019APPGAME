using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState : MonoBehaviour
{
    public ButtonManager manager;

    public virtual void Awake()
    {
        manager = GetComponent<ButtonManager>();
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }
}

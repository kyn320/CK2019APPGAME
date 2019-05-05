using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : Controller
{
    public void Jump()
    {
        GetTarget().SetState(UnitStateCode.JUMP);
    }

    public void Rush()
    {
        float dir = GetTarget().transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        GetTarget().ctrlRushDir = new Vector3(Mathf.Sin(dir), 0.0f, Mathf.Cos(dir));
        GetTarget().ctrlRushDistance = 1f;
        GetTarget().SetState(UnitStateCode.RUSH);
    }
}

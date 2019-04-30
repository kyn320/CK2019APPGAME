using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public UnitManager GetTarget()
    {
        return GameManager.Instance.localPlayer;
    }
}

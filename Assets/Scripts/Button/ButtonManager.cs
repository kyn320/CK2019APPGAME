using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ButtonStateCode
{
    IDLE = 0,
    WORK,
    FINISH
}

public class ButtonManager : MonoBehaviour
{
    public ButtonStateCode currentState = ButtonStateCode.FINISH;
    public MeshRenderer meshRenderer;
    public UnitManager target;
    public UnitManager occupationTarget;

    Dictionary<ButtonStateCode, ButtonState> states = new Dictionary<ButtonStateCode, ButtonState>();

    public float offsetY;
    public float occupationTime;
    public float occupationDepth;

    private void Awake()
    {
        states[ButtonStateCode.IDLE] = GetComponent<ButtonIdle>();
        states[ButtonStateCode.WORK] = GetComponent<ButtonWork>();
        states[ButtonStateCode.FINISH] = GetComponent<ButtonFinish>();

        meshRenderer = GetComponent<MeshRenderer>();
        offsetY = transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(ButtonStateCode.IDLE);
    }

    public void SetState(ButtonStateCode stateCode)
    {
        if (currentState == stateCode) return;

        foreach (ButtonState state in states.Values)
        {
            state.enabled = false;
        }
        states[currentState].Exit();
        currentState = stateCode;
        states[currentState].enabled = true;
        states[currentState].Enter();
    }
}

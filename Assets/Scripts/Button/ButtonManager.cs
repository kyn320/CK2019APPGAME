using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public enum ButtonStateCode
{
    IDLE = 0,
    WORK,
    FINISH
}

public class ButtonManager : MonoBehaviourPunCallbacks
{
    public ButtonStateCode currentState = ButtonStateCode.FINISH;
    public MeshRenderer meshRenderer;
    public MeshRenderer tokenMeshRenderer;
    public MeshRenderer crystalMeshRenderer;
    public MeshRenderer columnMeshRenderer;
    public UnitBuff buff;
    public List<UnitManager> targetList = new List<UnitManager>();
    public UnitManager target;
    public UnitManager occupationTarget;

    Dictionary<ButtonStateCode, ButtonState> states = new Dictionary<ButtonStateCode, ButtonState>();

    public float offsetY;
    public float occupationTime;
    public float occupationDepth;
    public float reoccupationTime;
    public bool isHidden = false;

    private void Awake()
    {
        states[ButtonStateCode.IDLE] = GetComponent<ButtonIdle>();
        states[ButtonStateCode.WORK] = GetComponent<ButtonWork>();
        states[ButtonStateCode.FINISH] = GetComponent<ButtonFinish>();

        meshRenderer = GetComponent<MeshRenderer>();
        buff = GetComponent<UnitBuff>();
        offsetY = transform.position.y;
        target = null;
        occupationTarget = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.buttons.Add(this);
        if (tokenMeshRenderer == null)
        {
            buff.isHidden();
            isHidden = true;
        }
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
        if(target && target.photonView.IsMine)
            photonView.RPC("PunButtonSetState", RpcTarget.Others, currentState);
    }

    public void PunSetState(ButtonStateCode stateCode)
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

    [PunRPC]
    public void PunButtonSetState(ButtonStateCode stateCode)
    {
        PunSetState(stateCode);
    }

    void UpdateState()
    {
        if(targetList.Count == 0)
        {
            SetState(ButtonStateCode.IDLE);
        }
        else if(targetList.Count == 1)
        {
            target = targetList[0];
            if (occupationTarget)
            {
                if (occupationTarget == target)
                    return;
                if (target.haveItem == null)
                    return;
            }
            SetState(ButtonStateCode.WORK);
        }
        else
        {
            SetState(ButtonStateCode.IDLE);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Equals("GroundChecker"))
        {
            targetList.Add(collision.gameObject.GetComponentInParent<UnitManager>());
            UpdateState();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name.Equals("GroundChecker"))
        {
            targetList.Remove(collision.gameObject.GetComponentInParent<UnitManager>());
            UpdateState();
        }
    }
}

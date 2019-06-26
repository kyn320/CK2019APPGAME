using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public enum ItemStateCode
{
    IDLE,
    PICK,
    DROP,
    REMOVE,
}

public class ItemManager : MonoBehaviourPunCallbacks
{
    public ItemStateCode currentState = ItemStateCode.IDLE;
    public Rigidbody rigidbody;

    Dictionary<ItemStateCode, ItemState> states = new Dictionary<ItemStateCode, ItemState>();

    public UnitManager target;
    public Vector3 spawnPosition;
    public float respawnTimer;

    private void Awake()
    {
        states[ItemStateCode.IDLE] = GetComponent<ItemIdle>();
        states[ItemStateCode.PICK] = GetComponent<ItemPick>();
        states[ItemStateCode.DROP] = GetComponent<ItemDrop>();
        states[ItemStateCode.REMOVE] = GetComponent<ItemRemove>();

        rigidbody = GetComponent<Rigidbody>();
        target = null;
        spawnPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(ItemStateCode.REMOVE);
    }

    public void SetState(ItemStateCode stateCode)
    {
        if (currentState == stateCode) return;

        foreach (ItemState state in states.Values)
        {
            state.enabled = false;
        }
        states[currentState].Exit();
        currentState = stateCode;
        states[currentState].enabled = true;
        states[currentState].Enter();

        int targetViewId = -1;

        if (target)
        {
            targetViewId = target.photonView.ViewID;
        }
        photonView.RPC("PunItemSetState", RpcTarget.Others, currentState, targetViewId);
    }

    //SetState()에서 RPC를 뺀 버전
    public void PunSetState(ItemStateCode stateCode)
    {
        if (currentState == stateCode) return;

        foreach (ItemState state in states.Values)
        {
            state.enabled = false;
        }
        states[currentState].Exit();
        currentState = stateCode;
        states[currentState].enabled = true;
        states[currentState].Enter();
    }

    //상대방 클라이언트에서 상태변경이 없을 경우를 대비해 동기화
    [PunRPC]
    public void PunItemSetState(ItemStateCode stateCode, int targetViewId)
    {
        if(targetViewId >= 0)
            target = PhotonView.Find(targetViewId).GetComponent<UnitManager>();
        PunSetState(stateCode);
    }
}

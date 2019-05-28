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

public class ItemManager : MonoBehaviour
{
    public ItemStateCode currentState = ItemStateCode.IDLE;
    public Rigidbody rigidbody;

    Dictionary<ItemStateCode, ItemState> states = new Dictionary<ItemStateCode, ItemState>();

    public UnitManager target;
    public Vector3 spawnPosition;

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
        PhotonView photonView = PhotonView.Get(GameManager.Instance.localPlayer);
        photonView.RPC("PunItemSetState", RpcTarget.Others, currentState);
    }

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

    [PunRPC]
    public void PunItemSetState(ItemStateCode stateCode)
    {
        PunSetState(stateCode);
    }
}

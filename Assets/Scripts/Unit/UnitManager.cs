using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public enum UnitStateCode
{
    IDLE = 0,
    MOVE,
    JUMP,
    RUSH,
    ROLL,
    FALL
}

public class UnitManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public UnitStateCode currentState = UnitStateCode.FALL;
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Rigidbody rigidbody;
    public Animator animator;
    public Vector3 spawnPosition;

    Dictionary<UnitStateCode, UnitState> states = new Dictionary<UnitStateCode, UnitState>();

    public Vector3 ctrlMoveDir;
    public float ctrlMoveDistance;
    public Vector3 ctrlRushDir;
    public float ctrlRushDistance;
    bool isSetState;

    public UnitStat stat;
    public bool haveItem;

    private void Awake()
    {
        states[UnitStateCode.IDLE] = GetComponent<UnitIdle>();
        states[UnitStateCode.MOVE] = GetComponent<UnitMove>();
        states[UnitStateCode.JUMP] = GetComponent<UnitJump>();
        states[UnitStateCode.RUSH] = GetComponent<UnitRush>();
        states[UnitStateCode.ROLL] = GetComponent<UnitRoll>();
        states[UnitStateCode.FALL] = GetComponent<UnitFall>();

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        haveItem = false;

        if (!photonView.IsMine)
        {
            rigidbody.useGravity = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(UnitStateCode.IDLE);
        if (!photonView.IsMine)
        {
            states[currentState].enabled = false;
        }
    }

    public void SetState(UnitStateCode stateCode)
    {
        if (isSetState) return;

        bool isLinked = false;
        foreach (UnitStateCode state in states[currentState].linkStates)
        {
            if (state == stateCode)
                isLinked = true;
        }

        //Debug.Log(photonView.Owner.NickName + " : " + isLinked);

        if (!isLinked) return;

        foreach (UnitState state in states.Values)
        {
            state.enabled = false;
        }

        states[currentState].Exit();
        currentState = stateCode;
        states[currentState].enabled = true;
        states[currentState].Enter();
        animator.SetInteger("currentState", (int)currentState);

        if (photonView.IsMine)
        {
            photonView.RPC("PunUnitSetState", RpcTarget.Others, currentState);
        }
        isSetState = true;
    }

    [PunRPC]
    void PunUnitSetState(UnitStateCode stateCode)
    {
        currentState = stateCode;
        animator.SetInteger("currentState", (int)currentState);
    }

    private void Update()
    {
        isSetState = false;
        if (transform.position.y < -5.0f)
        {
            SetState(UnitStateCode.FALL);
        }
    }

    void RollCheck(Collision collision)
    {
        UnitManager target = collision.gameObject.GetComponent<UnitManager>();
        if (target.currentState != UnitStateCode.RUSH) return;

        rigidbody.velocity = target.rigidbody.velocity * 1.2f * (1 - stat.rollResistance.Value * 0.01f);
        Vector3 tv = Quaternion.LookRotation(transform.position - target.transform.position).eulerAngles;
        float trY = tv.y;
        float rY = transform.rotation.eulerAngles.y;

        if (rY < 0.0f) rY += 360.0f;
        if (trY < 0.0f) trY += 360.0f;

        //back
        if (Mathf.Abs(rY - trY) > 90.0f)
        {
            Vector3 v = tv;
            v.y -= 180;
            transform.rotation = Quaternion.Euler(v);
            animator.SetBool("HitBack", true);
        }
        //front
        else
        {
            Vector3 v = tv;
            transform.rotation = Quaternion.Euler(v);
            animator.SetBool("HitBack", false);
        }
        SetState(UnitStateCode.ROLL);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!photonView.IsMine) return;
        if (currentState == UnitStateCode.ROLL) return;
        if (!collision.gameObject.CompareTag("Player")) return;
        RollCheck(collision);
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting && photonView.IsMine)
    //    {
    //        stream.SendNext(currentState);
    //    }
    //    else if(stream.IsReading && !photonView.IsMine)
    //    {
    //        SetState((UnitStateCode)stream.ReceiveNext());
    //    }
    //}
}

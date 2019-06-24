using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFall : UnitFall
{
    [SerializeField]
    private Transform groundChecker;

    private Collider collider;
    private Collider groundCollider;

    [FMODUnity.EventRef]
    public string chEventPath;
    public FMOD.Studio.EventInstance ch_medieval;

    [FMODUnity.EventRef]
    public string reEventPath;
    public FMOD.Studio.EventInstance re_medieval;

    [FMODUnity.EventRef]
    public string rechEventPath;
    public FMOD.Studio.EventInstance rech_medieval;

    public override void Awake()
    {
        base.Awake();

        linkStates.Add(UnitStateCode.IDLE);

        collider = GetComponent<Collider>();
        groundCollider = groundChecker.GetComponent<Collider>();

        ch_medieval = FMODUnity.RuntimeManager.CreateInstance(chEventPath);
        ch_medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));

        re_medieval = FMODUnity.RuntimeManager.CreateInstance(reEventPath);
        re_medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));

        rech_medieval = FMODUnity.RuntimeManager.CreateInstance(rechEventPath);
        rech_medieval.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
    }

    public override void Enter()
    {
        base.Enter();
        groundCollider.material = collider.material;
        ch_medieval.start();
    }

    public override void Exit()
    {
        base.Exit();
        groundCollider.material = null;
    }

    private void FixedUpdate()
    {
        if(transform.position.y < -50.0f)
        {
            manager.Respawn(Vector3.zero);
            if (manager.haveItem)
            {
                manager.haveItem.SetState(ItemStateCode.REMOVE);
            }
        }
        Collider[] cols = Physics.OverlapSphere(groundChecker.position, 0.25f, LayerMask.GetMask("Ground"));
        if (cols.Length > 0)
        {
            re_medieval.start();
            rech_medieval.start();
            manager.SetState(UnitStateCode.IDLE);
        }
    }
}

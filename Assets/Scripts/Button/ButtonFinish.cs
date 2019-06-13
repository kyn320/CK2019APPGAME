using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class ButtonFinish : ButtonState
{
    public override void Enter()
    {
        base.Enter();
        OccupationSetting();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Update()
    {
        Vector3 vector = transform.position;
        vector.y += manager.occupationDepth * 2.0f * Time.deltaTime;

        if(vector.y > manager.offsetY)
        {
            vector.y = manager.offsetY;
            manager.SetState(ButtonStateCode.IDLE);
        }
        transform.position = vector;
    }

    void OccupationSetting()
    {
        bool nonTarget = true;
        if (manager.occupationTarget != null && manager.target.haveItem)
        {
            nonTarget = false;
            manager.target.haveItem.SetState(ItemStateCode.REMOVE);
        }
        manager.occupationTarget = manager.target;
        manager.occupationTime = manager.reoccupationTime;
        manager.buff.Set(manager.occupationTarget);

        if (manager.isHidden)
        {
            GameObject.FindWithTag("DisplayBoard").GetComponent<GameUIScore>().UpdateGoldScore(manager.occupationTarget == GameManager.Instance.localPlayer);
        }
        else
        {
            GameObject.FindWithTag("DisplayBoard").GetComponent<GameUIScore>().UpdateScore();
        }

        string path;

        if (PhotonNetwork.OfflineMode)
            path = "zeus";
        else
            path = manager.target.photonView.Owner.CustomProperties["Type"] as string;

        if (manager.tokenMeshRenderer == null)
        {
            Material material = Resources.Load<Material>("Material/gold_button_" + path);
            manager.meshRenderer.material = material;
        }
        else
        {
            Material material = Resources.Load<Material>("Material/button_" + path);
            manager.meshRenderer.material = material;
            manager.tokenMeshRenderer.material = material;
            manager.columnMeshRenderer.material = Resources.Load<Material>("Material/column_" + path);
            manager.crystalMeshRenderer.material = Resources.Load<Material>("Material/crystal_" + path);
            manager.crystalMeshRenderer.gameObject.GetComponent<ObjectAniControl>().enabled = true;
        }
    }
}

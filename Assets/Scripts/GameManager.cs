using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : Singleton<GameManager>, IInRoomCallbacks, IMatchmakingCallbacks
{
    const float Min = 60f;

    public UnitManager localPlayer;

    [Header("플레이 시간(분)")]
    public int maxPlayTime = 1;

    private float playTime = Min * 1;
    public bool isBegin;

    public Camera backViewCamera;
    public Camera quarterViewCamera;

    public GameObject backViewController;
    public GameObject quarterViewController;

    protected override void Awake()
    {
        base.Awake();
        isBegin = true;

        if (!PhotonNetwork.OfflineMode)
        {
            localPlayer = PhotonNetwork.Instantiate(PhotonNetwork.LocalPlayer.CustomProperties["Type"] + "Player"
                , Vector3.zero
                , Quaternion.identity).GetComponent<UnitManager>();
            localPlayer.Respawn(Vector3.down * 25);
        }
        else
        {
            localPlayer = Instantiate(Resources.Load("RedPlayer") as GameObject
                , new Vector3(0.0f, 5.0f, 0.0f)
                , Quaternion.identity).GetComponent<UnitManager>();
        }

        backViewCamera.transform.parent = localPlayer.transform;
        //TODO :: 카메라 백뷰 위치 버그 있음
        backViewCamera.transform.localPosition += localPlayer.transform.localPosition;// + new Vector3(0, 5, -2.5f);
        backViewCamera.transform.eulerAngles += localPlayer.spawnRotation;
        backViewCamera.enabled = true;
        quarterViewCamera.enabled = false;
    }

    void Start()
    {
        playTime = Min * maxPlayTime;
    }

    private void Update()
    {
        playTime -= Time.deltaTime;
        if (playTime < 0f)
        {
            playTime = 0f;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public float GetPlayTime()
    {
        return playTime;
    }

    public void SwapCamera()
    {
        backViewCamera.enabled = !backViewCamera.enabled;
        quarterViewCamera.enabled = !quarterViewCamera.enabled;

        backViewController.SetActive(backViewCamera.enabled);
        quarterViewController.SetActive(quarterViewCamera.enabled);
    }

    #region RoomCallback

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName);
    }

    #endregion

    #region MatchingCallback
    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        throw new System.NotImplementedException();
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
        throw new System.NotImplementedException();
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        throw new System.NotImplementedException();
    }

    public void OnCreatedRoom()
    {
        throw new System.NotImplementedException();
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnJoinedRoom()
    {
        throw new System.NotImplementedException();
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}

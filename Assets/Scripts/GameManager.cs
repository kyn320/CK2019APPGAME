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

    protected override void Awake()
    {
        base.Awake();

        if (!PhotonNetwork.OfflineMode)
        {
            localPlayer = PhotonNetwork.Instantiate(PhotonNetwork.LocalPlayer.CustomProperties["Type"] + "Player"
                , Vector3.zero
                , Quaternion.identity).GetComponent<UnitManager>();
            localPlayer.transform.position = localPlayer.spawnPosition;
        }
        else
        {
            localPlayer = Instantiate(Resources.Load("RedPlayer") as GameObject
                , new Vector3(0.0f, 5.0f, 0.0f)
                , Quaternion.identity).GetComponent<UnitManager>();
        }

        Camera.main.transform.parent = localPlayer.transform;
        Camera.main.transform.localPosition += localPlayer.transform.position;
    }

    void Start()
    {
        playTime = Min * maxPlayTime;
    }

    private void Update()
    {
        print(PhotonNetwork.NickName);

        playTime -= Time.deltaTime;
        if (playTime < 0f)
        {
            playTime = 0f;
            Debug.Log("Game End");
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public float GetPlayTime() {
        return playTime;
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

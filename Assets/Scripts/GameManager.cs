using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

using UnityEngine.UI;

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

    public Dictionary<UnitStatCode, float> standardStat = new Dictionary<UnitStatCode, float>();
    public float[][] buffStatValue = new float[(int)UnitStatCode.SIZE][];
    public List<ButtonManager> buttons = new List<ButtonManager>();

    public Text buffText;

    protected override void Awake()
    {
        base.Awake();
        isBegin = true;

        standardStat[UnitStatCode.MOVE_SPEED] = 3.0f;
        standardStat[UnitStatCode.RUSH_POWER] = 10.0f;
        standardStat[UnitStatCode.JUMP_POWER] = 6.0f;
        standardStat[UnitStatCode.ROLL_RESISTANCE] = 100.0f;

        if (!PhotonNetwork.OfflineMode)
        {
            localPlayer = PhotonNetwork.Instantiate(PhotonNetwork.LocalPlayer.CustomProperties["Type"] + "Player"
                , Vector3.zero
                , Quaternion.identity).GetComponent<UnitManager>();
            //localPlayer.transform.position = localPlayer.spawnPosition + Vector3.up * 25;
        }
        else
        {
            localPlayer = Instantiate(Resources.Load("RedPlayer") as GameObject
                , new Vector3(0.0f, 5.0f, 0.0f)
                , Quaternion.identity).GetComponent<UnitManager>();
        }

        backViewCamera.transform.parent = localPlayer.transform;
        localPlayer.Respawn(Vector3.down * 25);
        //TODO :: 카메라 백뷰 위치 버그 있음
        //backViewCamera.transform.localPosition += localPlayer.transform.position;// + new Vector3(0, 5, -2.5f);
        //backViewCamera.transform.eulerAngles += localPlayer.spawnRotation;
        backViewCamera.enabled = false;
        quarterViewCamera.enabled = true;
        quarterViewCamera.transform.eulerAngles += localPlayer.transform.eulerAngles;
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
            SceneManager.LoadScene("ResultScene");
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

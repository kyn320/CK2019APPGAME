using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

using UnityEngine.UI;

public class GameManager : Singleton<GameManager>, IInRoomCallbacks, IMatchmakingCallbacks, IConnectionCallbacks
{
    const float Min = 60f;

    public bool offline = false;
    public UnitManager localPlayer;
    public UnitManager otherPlayer;

    [Header("플레이 시간(분)")]
    public int maxPlayTime = 1;

    private float playTime = Min * 1;
    public bool isBegin;
    
    public Camera quarterViewCamera;

    public GameObject backViewController;
    public GameObject quarterViewController;

    public Dictionary<UnitStatCode, float> standardStat = new Dictionary<UnitStatCode, float>();
    public BuffData[] buffStatValue = new BuffData[(int)UnitStatCode.SIZE];
    public List<ButtonManager> buttons = new List<ButtonManager>();
    public bool[] buffCheckFlag = new bool[(int)UnitStatCode.SIZE * 3];

    public BGMPlayer bgm;

    protected override void Awake()
    {
        base.Awake();
        isBegin = true;

        standardStat[UnitStatCode.MOVE_SPEED] = 3.0f;
        standardStat[UnitStatCode.RUSH_POWER] = 10.0f;
        standardStat[UnitStatCode.JUMP_POWER] = 6.0f;
        standardStat[UnitStatCode.ROLL_RESISTANCE] = 100.0f;

        PhotonNetwork.OfflineMode = offline;

        if (!PhotonNetwork.OfflineMode)
        {
            localPlayer = PhotonNetwork.Instantiate(PhotonNetwork.LocalPlayer.CustomProperties["Type"] + "Player"
                , Vector3.zero
                , Quaternion.identity).GetComponent<UnitManager>();
        }
        else
        {
            localPlayer = Instantiate(Resources.Load("zeusPlayer") as GameObject
                , Vector3.zero
                , Quaternion.identity).GetComponent<UnitManager>();
        }
        localPlayer.Respawn(Vector3.down * 25);
        quarterViewCamera.enabled = true;
        quarterViewCamera.transform.eulerAngles += localPlayer.transform.eulerAngles;
    }

    void Start()
    {
        playTime = Min * maxPlayTime;
    }

    public void Init()
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (unit.GetComponent<UnitManager>() != localPlayer)
            {
                otherPlayer = unit.GetComponent<UnitManager>();
            }
            Debug.Log("asdf : " + unit);
        }
    }

    private void Update()
    {
        if (!isBegin)
        {
            playTime -= Time.deltaTime;
            if (playTime < 0f)
            {
                playTime = 0f;
                LeaveRoom();
            }
        }
    }

    public void LeaveRoom()
    {
        int characterType = (PhotonNetwork.LocalPlayer.CustomProperties["Type"].Equals("zeus"))? 1 : 2;
        int gameResult = 0;
        int buttonCount = 0;
        int otherCount = 0;

        foreach(ButtonManager button in buttons)
        {
            if(button.occupationTarget == localPlayer)
            {
                buttonCount++;
            }
            else if(button.occupationTarget != null)
            {
                otherCount++;
            }
        }
        if (buttonCount > otherCount) gameResult = 1;

        PlayerPrefs.SetInt("CharacterType", characterType);
        PlayerPrefs.SetInt("GameResult", gameResult);
        PlayerPrefs.SetInt("ButtonCount", buttonCount);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("ResultScene");
    }

    public float GetPlayTime()
    {
        return playTime;
    }

    public void SwapCamera()
    {
        quarterViewCamera.enabled = !quarterViewCamera.enabled;
        
        quarterViewController.SetActive(quarterViewCamera.enabled);
    }

    public void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit()");
        //PhotonNetwork.Disconnect();
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

    #region ConnectCallback

    public void OnConnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnConnectedToMaster()
    {
        throw new System.NotImplementedException();
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected() cause : " + cause);
        throw new System.NotImplementedException();
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        throw new System.NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        throw new System.NotImplementedException();
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}

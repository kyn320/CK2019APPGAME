using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Resume.Network
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        string gameVersion = "1";
        bool isConnecting;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            Connect();
        }

        public void Connect()
        {
            isConnecting = true;
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("OnDisconnected() : reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed()");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom()");

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("OnPlayerEnteredRoom()");

            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                int randType = Random.Range(1, 3);

                PhotonNetwork.CurrentRoom.GetPlayer(randType).NickName = "Red";
                PhotonNetwork.CurrentRoom.GetPlayer(randType == 1 ? 2 : 1).NickName = "Blue";

                PhotonNetwork.LoadLevel("GameScene");
            }
        }
    }
}

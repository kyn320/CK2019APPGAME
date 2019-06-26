using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Resume.Network
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        //Room 최대 인원
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
            //연결상태 확인
            if (PhotonNetwork.IsConnected)
            {
                //생선된 방 중 아무거나 입장
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

            //중복 입장 예외처리
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

            //마스터 클라이언트가 해당 방에 유저가 다 들어오면 게임 시작
            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //시작 전 캐릭터 타입 동기화
                int randType = Random.Range(1, 3);

                ExitGames.Client.Photon.Hashtable table = new ExitGames.Client.Photon.Hashtable();

                table["Type"] = "hera";

                PhotonNetwork.CurrentRoom.GetPlayer(randType).SetCustomProperties(table);

                table["Type"] = "zeus";

                PhotonNetwork.CurrentRoom.GetPlayer(randType == 1 ? 2 : 1).SetCustomProperties(table);

                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel("GameScene");
            }
        }
    }
}

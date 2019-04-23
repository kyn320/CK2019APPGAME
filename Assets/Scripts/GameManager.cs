using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public UnitManager player;
    public float gameTime = 3 * 60.0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        player = PhotonNetwork.Instantiate(PhotonNetwork.NickName + "Player", new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity, 0).GetComponent<UnitManager>();
        Camera.main.transform.parent = player.transform;
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void Update()
    {
        gameTime -= Time.deltaTime;
        if(gameTime < 0.0f)
        {
            gameTime = 0.0f;
            Debug.Log("Game End");
        }
    }
}

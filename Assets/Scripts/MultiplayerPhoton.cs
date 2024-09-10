using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;

public class MultiplayerPhoton : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI userList;

    void Start()
    {
        userList.text = "";
        Debug.Log("[MultiplayerPhoton] Conectando no servidor...");
        //atualizar a versao  do photon para separar
        //os jogadores em servidores diferentes
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.NickName = Environment.UserName;

        //conecta o servidor no photon usando
        //o App ID que esta na pasta
        //Assets/Photon/PhotonUnityNetwork/Resources
        PhotonNetwork.ConnectUsingSettings();
    }



    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("[MultiplayerPhoton] Conectado no servidor!");

        //agora que estamos conectados no servidor do Photon
        //precisamos entrar no lobby para receber a lista de
        //salas ou criar uma sala propria
        Debug.Log("[MultiplayerPhoton] Entrando no lobby...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("[MultiplayerPhoton] Entrou no lobby!");

        //agora que estamos em um lobby
        //podemos entrar numa sala ou criar uma se for necessario

        Debug.Log("[MultiplayerPhoton] Entrando ou criando uma sala...");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("CapivaraRoom", roomOptions, null);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        string log = "";
        foreach (RoomInfo room in roomList)
        {
            log += "ROOM: " + room.Name + "\n";
        }
        Debug.Log("[MultiplayerPhoton] RoomListUpdate: " + log);
    }

    public override void OnJoinedRoom()
    {
        //esse comando vai rodar quando EU entrar na sala
        Debug.Log("[MultiplayerPhoton] Entrei na sala!");

        UpdateUserList();

        //PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //esse comando vai rodar quando outro jogador entrar na sala
        Debug.Log("[MultiplayerPhoton] Player: " + newPlayer.NickName + "entrou na sala!");

        UpdateUserList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //jogador saiu da sala
        base.OnPlayerLeftRoom(otherPlayer);

        userList.text = userList.text.Replace(otherPlayer.NickName + "\n", "");
    }

    public void UpdateUserList()
    {
        userList.text = "";
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            userList.text += player.NickName + "\n";
        }
    }
}

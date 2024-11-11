using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MultiplayerPhoton : MonoBehaviourPunCallbacks
{
    public GameObject mainMenu;
    public GameObject roomMenu;
    public GameObject loadingPanel;

    private bool onMainMenu = true;
    private bool onLobby = false;
    private string roomName;
    private Room room;
    private GameObject player;

    [SerializeField]
    private TextMeshProUGUI userList;
    [SerializeField]
    private TextMeshProUGUI roomNameTxt;
    [SerializeField]
    private TMP_InputField roomInput;

    public TaskManager task;

    void Start()
    {
        mainMenu.SetActive(true);
        roomMenu.SetActive(false);
        loadingPanel.SetActive(false);
        roomNameTxt.text = "";
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


    //  BOTOES

    public void CreateRoomBTN()
    {
        Debug.Log("[MultiplayerPhoton] Entrando ou criando uma sala...");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        if (roomInput.text.Trim() == "")
            roomName = PhotonNetwork.NickName.ToUpper() + " ROOM";
        else
            roomName = roomInput.text.Trim().ToUpper();
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
        room = new Room(roomName, roomOptions);
        roomInput.enabled = false;
        roomInput.text = "";
    }

    public void LeaveRoomBTN()
    {
        PhotonNetwork.LeaveRoom();

    }

    public void LobbyBTN()
    {
        onMainMenu = false;
        if (onLobby)
        {
            mainMenu.SetActive(false);
            roomMenu.SetActive(true);
        }
        else
        {
            loadingPanel.SetActive(true);
        }
        Debug.Log("botao lobby");
    }

    public void CharSelectBTN(int value)
    {
        int index = value;
        if (!PhotonNetwork.IsMasterClient)
            index += 2;

        GameObject.Find("PersistentInfo").GetComponent<PersistentInfo>().SetIndex(index);
    }

    // FUNCOES

    public void UpdateUserList()
    {
        userList.text = "";
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            userList.text += player.NickName + "\n";
        }
    }


    // FUNCOES PHOTON

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
        onLobby = true;
        if (!onMainMenu)
        {
            roomMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        Debug.Log("[MultiplayerPhoton] Entrou no lobby!");

        //agora que estamos em um lobby
        //podemos entrar numa sala ou criar uma se for necessario

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

    public override void OnCreatedRoom()
    {
        //this.player.isHost = true;        <-------------
    }

    public override void OnJoinedRoom()
    {
        //esse comando vai rodar quando EU entrar na sala
        Debug.Log("[MultiplayerPhoton] Entrei na sala!");

        roomNameTxt.enabled = true;

        roomNameTxt.text = room.Name;

        UpdateUserList();


        //this.player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);     <-------------
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //esse comando vai rodar quando outro jogador entrar na sala
        Debug.Log("[MultiplayerPhoton] Player: " + newPlayer.NickName + "entrou na sala!");

        UpdateUserList();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Exclui o jogador que saiu e avisa que todos devem ir ao lobby
        Debug.Log("[MultiplayerPhoton] O jogador " + otherPlayer.NickName + " saiu da sala. Expulsando todos.");

        // Deixa a sala e carrega a cena do lobby para todos os jogadores
        PhotonNetwork.LeaveRoom();

        userList.text = userList.text.Replace(otherPlayer.NickName + "\n", "");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("[MultiplayerPhoton] Todos os jogadores foram expulsos e estão voltando ao lobby.");
        PhotonNetwork.LoadLevel(0);
        Debug.Log("Eu sai da sala");
        roomNameTxt.enabled = false;
        roomInput.enabled = true;
        roomName = "";
        userList.text = "";
        room = null;
    }
    public void RestartGame()
    {
        // Chama o RPC para reiniciar o jogo em todos os jogadores
        photonView.RPC("RestartGameRPC", RpcTarget.All);
    }

    [PunRPC]
    public void RestartGameRPC()
    {
        // Carrega a cena atual para reiniciar o jogo
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        task.pause = false;
    }

    public void MainMenu()
    {
        // Deixar a sala atual e voltar ao lobby
        PhotonNetwork.LeaveRoom();
    }


}

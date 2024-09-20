using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Button startButton; // Bot�o que ser� usado para iniciar o jogo

    void Start()
    {
        // Desativa ou ativa o bot�o Start conforme esteja na sala ou n�o
        UpdateStartButtonState();
    }

    void UpdateStartButtonState()
    {
        // Verifica se o jogador est� em uma sala
        if (PhotonNetwork.InRoom)
        {
            // Verifica se � o fod�o (dono da sala)
            if (PhotonNetwork.IsMasterClient)
            {
                startButton.gameObject.SetActive(true); // Ativa o bot�o de Start se for o criador da sala
            }
            else
            {
                startButton.gameObject.SetActive(false); // Esconde o bot�o para outros jogadores
            }
        }
        else
        {
            // Se n�o estiver em nenhuma sala, desativa o bot�o Start
            startButton.gameObject.SetActive(false);
        }
    }

    // M�todo que chama quando clica em Start (inicia o jogo pra todos)
    public void OnStartButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Garante que o jogo comece para todos
            photonView.RPC("StartGame", RpcTarget.All);
        }
    }

    [PunRPC]
    void StartGame()
    {
        PhotonNetwork.LoadLevel("Scene1"); // Carrega a cena do jogo para todos na sala
    }

    // Chama automaticamente quando o Master Client muda
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // Muda o bot�o de Start com base em quem � o novo Master Client
        Debug.Log("[LobbyManager] MasterClient mudou. Novo Master: " + newMasterClient.NickName);
        UpdateStartButtonState();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[LobbyManager] Entrou na sala.");
        // Atualiza o bot�o de Start quando o jogador entra na sala
        UpdateStartButtonState();
    }

    // Chama quando outro jogador entra na sala
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("[LobbyManager] Jogador entrou na sala: " + newPlayer.NickName);
        // Atualiza o bot�o de Start quando um novo jogador entra na sala (no Master Client)
        UpdateStartButtonState();
    }

    // Chamado quando o jogador sai da sala
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("[LobbyManager] Jogador saiu da sala: " + otherPlayer.NickName);
        // Atualiza o bot�o de Start quando um jogador sai da sala
        UpdateStartButtonState();
    }

    // Chamado quando o jogador local sai da sala
    public override void OnLeftRoom()
    {
        Debug.Log("[LobbyManager] Saiu da sala.");
        // Garante que o bot�o seja desativado ao sair da sala
        startButton.gameObject.SetActive(false);
    }
}

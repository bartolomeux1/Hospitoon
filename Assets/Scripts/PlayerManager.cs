using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            // Instancia o player apenas para este jogador
            GameObject playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);

            // Verifica se este � o jogador local e configura a c�mera
            if (playerInstance.GetComponent<PhotonView>().IsMine)
            {
                virtualCamera.Follow = playerInstance.transform;
            }

            int index = GameObject.Find("PersistentInfo").GetComponent<PersistentInfo>().GetIndex();
            playerInstance.GetComponent<SkinController>().CallChangeSkin(index);
        }
    }
}

using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CharacterMovement : MonoBehaviourPunCallbacks
{
    private CharacterController character;
    private Vector3 inputX;

    public GameObject bisturi;
    public GameObject siringa;

    public bool siringaMao;
    public bool bisturiMao;

    public float velocity = 10.0f;

    public Game game;

    void Start()
    {
        character = GetComponent<CharacterController>();

        // Verifica se o player não é controlado pelo cliente local
        if (!photonView.IsMine)
        {
            Destroy(character); // Opcional: Destruir o controller para outros jogadores
            return;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        inputX = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputX = Vector3.ClampMagnitude(inputX, 1);
        character.Move(inputX * velocity * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return; // Garante que apenas o player local pode interagir

        if (other.gameObject.tag == "Bisturi")
        {
            Debug.Log("collected");
            bisturi.SetActive(true);
            bisturiMao = true;

            if (siringa.activeSelf)
            {
                siringa.SetActive(false);
                siringaMao = false;
            }
        }

        if (other.gameObject.tag == "Siringa")
        {
            siringa.SetActive(true);
            siringaMao = true;

            if (bisturi.activeSelf)
            {
                bisturi.SetActive(false);
                bisturiMao = false;
            }
        }
        if (!photonView.IsMine) { }
    }
}

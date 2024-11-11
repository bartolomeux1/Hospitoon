using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviourPunCallbacks
{
    private CharacterController character;
    private Vector3 inputX;
    private Vector3 buffer = Vector3.zero;

    public GameObject bisturi;
    public GameObject siringa;
    public GameObject remedio;

    public bool siringaMao;
    public bool bisturiMao;
    public bool remedioMao;

    private Animator animController;
    private GameObject graphicPlayer;
    private SpriteRenderer spriteRenderer;

    public float velocity = 10.0f;

    public Game game;
    public SirurgiaMinigame sirurgiaMinigame;

    // Adicione uma referência para a câmera
    

    void Start()
    {
        character = GetComponent<CharacterController>();
        // find the animator component in graphicPlayer
        graphicPlayer = transform.GetChild(3).gameObject;
        animController = graphicPlayer.GetComponent<Animator>();
        spriteRenderer = graphicPlayer.GetComponent<SpriteRenderer>();

        // Localiza o script 'Game' na 'mainCamera'
        GameObject cameraObj = GameObject.FindWithTag("Scripts");
        if (cameraObj != null)
        {
            game = cameraObj.GetComponent<Game>();
            // Obtém a referência do script CameraFollow2D
        }

        // Verifica se o player não é controlado pelo cliente local
        if (!photonView.IsMine)
        {
            Destroy(character); // Opcional: Destruir o controller para outros jogadores
            return;
        }
    }

    void FixedUpdate()
    {
        //magnitude, normalized

        if ((transform.position - buffer).magnitude > 0.01f)
        {
            animController.SetBool("isWalking", true);

            if ((transform.position - buffer).normalized == Vector3.up)
            {
                animController.Play("WalkUp");
            }
            else if ((transform.position - buffer).normalized == Vector3.down)
            {
                animController.Play("WalkDown");
            }
            if ((transform.position - buffer).normalized == Vector3.right)
            {
                spriteRenderer.flipX = false;
                animController.Play("WalkSide");
            }
            else if ((transform.position - buffer).normalized == Vector3.left)
            {
                spriteRenderer.flipX = true;
                animController.Play("WalkSide");
            }
        }
        else
        {
            animController.SetBool("isWalking", false);
        }


        buffer = transform.position;
        if (!photonView.IsMine) return;

        if (game.feedbackStatus == false)
        {
            return;
        }
        else
        {
            inputX = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            inputX = Vector3.ClampMagnitude(inputX, 1);
            character.Move(inputX * velocity * Time.deltaTime);

            /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                animController.SetBool("isWalking", true);
                if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
                {
                    animController.Play("WalkDown");
                }
                else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    animController.Play("WalkUp");
                }
                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    animController.Play("WalkDown");
                }
                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
                {
                    animController.Play("WalkSide");
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    animController.Play("WalkSide");
                    spriteRenderer.flipX = true;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    animController.Play("WalkSide");
                    spriteRenderer.flipX = false;
                }
            }
            else
                animController.SetBool("isWalking", false);*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return; // Garante que apenas o player local pode interagir

        if (other.gameObject.CompareTag("Bisturi"))
        {
            Debug.Log("Collected bisturi");
            photonView.RPC("CollectItem", RpcTarget.All, "Bisturi");
        }

        if (other.gameObject.CompareTag("Siringa"))
        {
            Debug.Log("Collected siringa");
            photonView.RPC("CollectItem", RpcTarget.All, "Siringa");
        }
        if (other.gameObject.CompareTag("Remedio"))
        {
            photonView.RPC("CollectItem", RpcTarget.All, "Remedio");
        }

        // Verifica se colidiu com outro jogador
        if (other.gameObject.CompareTag("Player") && other.gameObject != gameObject)
        {
            Vector3 pushDirection = (transform.position - other.transform.position).normalized;
            character.Move(pushDirection * velocity * Time.deltaTime); // Afasta o jogador colidido
        }
    }

    [PunRPC]
    void CollectItem(string item)
    {
        if (item == "Bisturi")
        {
            bisturi.SetActive(true);
            bisturiMao = true;
            game.podeAddTimer = true;

            if (siringa.activeSelf)
            {
                siringa.SetActive(false);
                siringaMao = false;
            }

            if (remedio.activeSelf)
            {
                remedio.SetActive(false);
                remedioMao = false;
            }
        }

        if (item == "Siringa")
        {
            siringa.SetActive(true);
            siringaMao = true;
            game.podeAddTimer = true;

            if (bisturi.activeSelf)
            {
                bisturi.SetActive(false);
                bisturiMao = false;
            }
            if (remedio.activeSelf)
            {
                remedio.SetActive(false);
                remedioMao = false;
            }
        }
        if (item == "Remedio")
        {
            remedio.SetActive(true);
            remedioMao = true;
            game.podeAddTimer = true;

            if (siringa.activeSelf)
            {
                siringa.SetActive(false);
                siringaMao = false;
            }

            if (bisturi.activeSelf)
            {
                bisturi.SetActive(false);
                bisturiMao = false;
            }
        }
    }
}

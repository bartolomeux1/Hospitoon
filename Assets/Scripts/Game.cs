using Photon.Pun;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("paciente gameobject")]

    public GameObject paciente1;
    public GameObject paciente1Spawn;
    public GameObject pacient1clone;

    public GameObject paciente2;
    public GameObject paciente2Spawn;
    public GameObject paciente2Clone;

    [Header("tasks ui")]

    public GameObject taskUi1;
    public GameObject taskUi2;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;

    public GameObject task2Object1Ui;
    public GameObject task2Object2Ui;

    [Header("tasks booleans")]

    public bool objective1Completed = false;
    public bool objective2Completed = false;

    public bool objective1Completed2 = false;
    public bool objective2Completed2 = false;

    public bool task1Completed = false;
    public bool task2Completed = false;

    ///vars eric
    [Header("Timer Setup")]
    public Text txtFeedback;
    public Text timerText;
    public Text pontuacaoText;
    public GameObject pontuacaoGrande;
    public Text pontuacaoGrandeText;
    public Slider progressTimerText;
    private float timer = 0.0f;
    public float pontuacao;
    public float maxTimer = 60f;
    public GameObject feedback;
    public bool feedbackStatus;
    private bool isTimerRunning = false;
    public CharacterMovement move;
    public Slider sliderTime;
    public Color corSlider;
    public Image imageFill;
    public bool podeAddTimer = false;
    public List<CharacterMovement> players; // Lista para armazenar referências aos jogadores
    public bool checkSiringa;
    public bool checkBisturi;

    public GameObject restartButton;
    public GameObject mainMenuButton;
    void Start()
    {
        pontuacaoGrande.SetActive(false);
        feedbackStatus = true;
        // Instanciar o player na rede usando o Photon
        if (PhotonNetwork.IsConnected)
        {
            // Certifique-se de que o prefab esteja localizado no Resources do Unity
            PhotonNetwork.Instantiate("Player", paciente1Spawn.transform.position, Quaternion.identity);
        }

        Invoke("Maca1Task1", 1);
        Invoke("Maca2Task1", 3);
        imageFill = sliderTime.fillRect.GetComponent<Image>();

        players = new List<CharacterMovement>();
        // faz a barra começar em  0
        progressTimerText.value = 0;
        progressTimerText.maxValue = 1; //faz ela terminar em 1
    }
    void FixedUpdate()
    {
        TimerRun();
        FeedBackStatus();
        Macas();
        pontuacaoGrandeText.text = "Pontuação: "+ pontuacao.ToString();

    }

    public void Macas()
    {
        //maca 1

        if (objective1Completed)
        {
            taskObject1Ui.SetActive(false);

        }
        else taskObject1Ui.SetActive(true);

        if (objective2Completed)
        {
            taskObject2Ui.SetActive(false);

        }
        else taskObject2Ui.SetActive(true);

        if (objective1Completed && objective2Completed)
        {
            task1Completed = true;

            taskUi1.SetActive(false);

        }
        if (task1Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            Invoke("Maca1Task1", 3);
            objective1Completed = false;
            objective2Completed = false;
            Destroy(pacient1clone);
            task1Completed = false;
        }

        //maca 2

        if (objective1Completed2)
        {
            task2Object1Ui.SetActive(false);

        }
        else task2Object1Ui.SetActive(true);

        if (objective2Completed2)
        {
            task2Object2Ui.SetActive(false);


        }
        else task2Object2Ui.SetActive(true);

        if (objective1Completed2 && objective2Completed2)
        {
            task2Completed = true;
            taskUi2.SetActive(false);
        }
        if (task2Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            Invoke("Maca2Task1", 3);
            objective1Completed2 = false;
            objective2Completed2 = false;
            Destroy(paciente2Clone);
            task2Completed = false;
        }
    }
    public void Maca1Task1()
    {
        Instantiate(paciente1, paciente1Spawn.transform.position, paciente1Spawn.transform.rotation);
        taskUi1.SetActive(true);

        isTimerRunning = true;

        pacient1clone = GameObject.Find("Paciente(Clone)");
    }
    public void Maca2Task1()
    {
        Instantiate(paciente2, paciente2Spawn.transform.position, paciente2Spawn.transform.rotation);
        taskUi2.SetActive(true);

        paciente2Clone = GameObject.Find("Paciente2(Clone)");
    }

    public void TimerRun()
    {
        if (isTimerRunning)
        {
            timer = 10;
            maxTimer -= Time.deltaTime;

            // Atualiza o valor do timer no texto
            timerText.text = "Time: " + Mathf.FloorToInt(maxTimer).ToString();
            pontuacaoText.text = pontuacao.ToString();
            // Calcula o progresso para a barra (normalizando entre 0 e 1)
            float progress = Mathf.Clamp01(maxTimer / timer);
            progressTimerText.value = progress;

            // Se o timer atingir 0, parar o progresso
            if (maxTimer <= 0)
            {
                timerText.text = "Time: 0";

                imageFill.color = corSlider; //muda a cor pra preto
                isTimerRunning = false;  // Para o timer
                feedbackStatus = false;
                feedback.SetActive(true);
                pontuacaoGrande.SetActive(true);
            }
        }

    }
    public void AddPontuacao(int n)
    {
        pontuacao += n;
    }
    public void FeedBackStatus()
    {
        if (feedbackStatus)
            txtFeedback.text = "Você ganhou";
        else txtFeedback.text = "Você perdeu";
    }


    public void AddTimer()
    {
        maxTimer += 3;

    }
}

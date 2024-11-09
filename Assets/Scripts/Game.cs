using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public TaskManager taskManager;

    public GameObject playerSpawn;
    ///vars eric
    [Header("Timer Setup")]
    public Text txtFeedback;
    public Text timerText;
    public TextMeshProUGUI pontuacaoText;
    public GameObject pontuacaoGrande;
    public TextMeshProUGUI pontuacaoGrandeText;
    public Slider progressTimerText;
    private float timer = 0.0f;
    public float pontuacao;
    public float maxTimer = 60f;
    public GameObject feedback;
    public bool feedbackStatus;
    public bool isTimerRunning = false;
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

        maxTimer = 30f;
       

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
        taskManager.Tasks();
        pontuacaoGrandeText.text = "Voce fez: "+ pontuacao.ToString() + " pontos.";

    }
    public void TimerRun()
    {
        if (isTimerRunning)
        {
            timer = 30;
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


    public void AddTimer(int addTimer)
    {
        maxTimer += addTimer;

    }
}

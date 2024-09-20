using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject paciente1;
    public GameObject pacienteSpawn;
    public GameObject TaskUi1;

    public GameObject taskObject1;
    public GameObject taskObject2;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;

    public bool objective1Completed = false;
    public bool objective2Completed = false;

    bool task1Completed = false;
    public SphereCollider macaCollider;

    ///vars eric
    [Header("Timer Setup")]
    public Text txtFeedback;
    public Text timerText;
    public Slider progressTimerText;
    private float timer = 0.0f;
    public float maxTimer = 10f;
    public GameObject feedback;
    public bool feedbackStatus;
    private bool isTimerRunning = false;


    void Start()
    {
        // start task 1 after 5 seconds
        Invoke("Task1", 5);

        // faz a barra começar em  0
        progressTimerText.value = 0;
        progressTimerText.maxValue = 1; //faz ela terminar em 1
    }
    void Update()
    {
        TimerRun();
        FeedBackStatus();

    }
    public void Task1()
    {
        Instantiate(paciente1, pacienteSpawn.transform.position, pacienteSpawn.transform.rotation);
        TaskUi1.SetActive(true);

        isTimerRunning = true;

        if (objective1Completed && objective2Completed)
        {
            feedbackStatus = true;
            feedback.SetActive(true);
            task1Completed = true;
            TaskUi1.SetActive(false);
            isTimerRunning = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (taskObject1.activeSelf)
            {
                taskObject1.SetActive(false);
                taskObject1Ui.SetActive(false);
                objective1Completed = true;
            }
            if (taskObject2.activeSelf)
            {
                taskObject2.SetActive(false);
                taskObject2Ui.SetActive(false);
                objective2Completed = true;
            }
        }
    }

    public void TimerRun()
    {
        if (isTimerRunning)
        {

            timer += Time.deltaTime;

            // Atualiza o valor do timer no texto
            timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();

            // Calcula o progresso para a barra (normalizando entre 0 e 1)
            float progress = Mathf.Clamp01(timer / maxTimer);
            progressTimerText.value = progress;

            // Se o timer atingir o tempo máximo, pode parar o progresso
            if (timer >= maxTimer)
            {
                isTimerRunning = false;  // Para o timer se atingir 90 segundos
                feedbackStatus = false;
                feedback.SetActive(true);
            }
        }
    }
    public void FeedBackStatus()
    {
        if (feedbackStatus)
            txtFeedback.text = "Você ganhou";
        else txtFeedback.text = "Você perdeu";
    }


}

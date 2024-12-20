using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SirurgiaMinigame : MonoBehaviour
{
    public TaskManager taskManager;
    public Game game;

    public GameObject task1;
    public GameObject task2;
    public GameObject task3;

    public GameObject sirurgiaMinigame;
    public Paciente paciente1;
    public Paciente paciente2;

    public bool cirurgia1Started = false;
    public bool cirurgia2Started = false;

    public Text objective;

    public bool cirurgiaCompleted = false;
    public bool cirurgiaFailed = false;

    public int cutpoints1;
    public int stitchPoints;
    public int stitchPoints2;

    public bool objective1Started = true;
    public bool objective2Started = false;
    public bool objective3Started = false;

    public bool objective1Completed = false;
    public bool objective2Completed = false;
    public bool objective3Completed = false;

    [Header("Buttons")]
    public GameObject button1task1;
    public GameObject button2task1;
    public GameObject button3task1;
    public GameObject button4task1;
    public GameObject button5task1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cutpoints1 == 5) 
        {
            cutpoints1++;
            objective1Completed = true;
            objective.text = "objetivo 1 concluido";
            Invoke("Task2", 1);
            Invoke("ReturnButtons", 1);
        }
        if (stitchPoints == 5)
        {
            stitchPoints++;
            objective2Completed = true;
            objective.text = "objetivo 2 concluido";
            Invoke("Task3", 1);
            Invoke("ReturnButtons", 1);
        }
        if (stitchPoints2 == 5)
        {
            stitchPoints2++;
            objective3Completed = true;
            cirurgiaCompleted = true;
            objective.text = "cirurgia feita com sucesso";
            Invoke("EndMiniGame", 1);
        }
        if (cirurgia1Started)
        {
            if (taskManager.timerAtual1 <= 0)
            {
                FailedMinigame();
            }
        }
        if(cirurgia2Started)
        {
            if (taskManager.timerAtual2 <= 0)
            {
                FailedMinigame();
            }
        }
        if (game.maxTimer <= 0)
        {
            CloseMiniGame();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            CloseMiniGame();
        }
    }
    private void Task2()
    {
        objective.text = "clique nos quadrados brancos para costurar a hemorragia interna";
        objective2Started = true;
        objective1Started = false;
    }
    private void Task3()
    {
        objective.text = "clique nos quadrados brancos para costurar a pele e finalizar a cirurgia";
        objective3Started = true;
        objective2Started = false;
    }
    private void FailedMinigame()
    {
        cirurgiaFailed = true;
        objective.text = "cirurgia falhou";
        HideButtons();
        Invoke("EndMiniGame", 1);
    }
    private void EndMiniGame()
    {
        //paciente1.SirurgiaCompleted = false;
        //paciente2.SirurgiaCompleted = false;
        cirurgiaCompleted = false;
        cirurgiaFailed = false;
        cirurgia1Started = false;
        cirurgia2Started = false;
        cutpoints1 = 0;
        stitchPoints = 0;
        stitchPoints2 = 0;
        ReturnButtons();
        objective1Started = true;
        objective2Started = false;
        objective3Started = false;
        objective1Completed = false;
        objective2Completed = false;
        objective3Completed = false;
        objective.text = "clique nos quadrados brancos para cortar a pele";
        sirurgiaMinigame.SetActive(false);
    }
    private void ReturnButtons()
    {
        button1task1.SetActive(true);
        button2task1.SetActive(true);
        button3task1.SetActive(true);
        button4task1.SetActive(true);
        button5task1.SetActive(true);
    }
    private void HideButtons()
    {
        button1task1.SetActive(false);
        button2task1.SetActive(false);
        button3task1.SetActive(false);
        button4task1.SetActive(false);
        button5task1.SetActive(false);
    }
    public void CloseMiniGame()
    {
        sirurgiaMinigame.SetActive(false);
    }
}

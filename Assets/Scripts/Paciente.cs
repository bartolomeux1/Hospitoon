using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour
{
    public bool HassCirurgia;
    public bool cirugiaStarted;
    public bool cirurgiaFailed;
    public bool cirurgiaCompleted;

    public Game game;
    public TaskManager taskManager;

    public GameObject sirurgiaMiniGameObj;
    public GameObject cirurgiaMiniGameText;
    public SirurgiaMinigame sirurgiaMiniGame;

    public string objectTag1;
    public string objectTag2;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;
    public GameObject text;

    public bool objective1Completed = false;
    public bool objective2Completed = false;
    public bool taskCompleted = false;

    public GameObject nextPaciente;
    public bool proximoPaciente1 = false;
    public bool proximoPaciente2 = false;
    public bool proximoPaciente3 = false;
    public bool proximoPaciente4 = false;
    public Animator animator;

    public int pacienteInt = 0;
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == objectTag1)
        {
            objective1Completed = true;
            taskObject1Ui.SetActive(false);
            if (pacienteInt == 1)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual1 >= 8)
                        taskManager.timerAtual1 = 10;
                    else
                        taskManager.timerAtual1 += 2;
                }
                else
                    taskManager.timerAtual1 += 3;
            }
            if (pacienteInt == 2)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual2 >= 8)
                        taskManager.timerAtual2 = 10;
                    else
                        taskManager.timerAtual2 += 2;
                }
                else
                    taskManager.timerAtual2 += 3;
            }
            if (pacienteInt == 3)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual3 >= 8)
                        taskManager.timerAtual3 = 10;
                    else
                        taskManager.timerAtual3 += 2;
                }
                else
                    taskManager.timerAtual3 += 3;               
            }
            if (pacienteInt == 4)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual4 >= 8)
                        taskManager.timerAtual4 = 10;
                    else
                        taskManager.timerAtual4 += 2;
                }
                else
                    taskManager.timerAtual4 += 3;
            }
            if (game.podeAddTimer == true)
            {
                game.AddTimer(2); //adiciona o tempo ao interagir com paciente
                if (taskManager.checkBisturi == false)
                {

                    game.AddPontuacao(1); //adiciona 1 na pontuação
                    taskManager.checkSiringa = true;
                }
                if (taskManager.checkBisturi == true)
                    game.AddPontuacao(1);
            }

            game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();

            collision.gameObject.SetActive(false);

            Debug.Log("SiringaUsada");
        }
        if (collision.gameObject.tag == objectTag2)
        {

            objective2Completed = true;
            taskObject2Ui.SetActive(false);

            if (pacienteInt == 1)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual1 >= 8)
                        taskManager.timerAtual1 = 10;
                    else
                        taskManager.timerAtual1 += 2;
                }
                else 
                    taskManager.timerAtual1 += 3;
            }
            if (pacienteInt == 2)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual2 >= 8)
                        taskManager.timerAtual2 = 10;
                    else
                        taskManager.timerAtual2 += 2;
                }
                else
                    taskManager.timerAtual2 += 3;
            }
            if (pacienteInt == 3)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual3 >= 8)
                        taskManager.timerAtual3 = 10;
                    else
                        taskManager.timerAtual3 += 2;
                }
                else
                    taskManager.timerAtual3 += 3;               
            }
            if (pacienteInt == 4)
            {
                if (!HassCirurgia)
                {
                    if (taskManager.timerAtual4 >= 8)
                        taskManager.timerAtual4 = 10;
                    else
                        taskManager.timerAtual4 += 2;
                }
                else
                    taskManager.timerAtual4 += 3;
            }
            if (game.podeAddTimer == true)
            {
                game.AddTimer(2); //adiciona o tempo ao interagir com paciente
                if (taskManager.checkSiringa == false)
                {

                    game.AddPontuacao(1); //adiciona 1 na pontuação
                    taskManager.checkBisturi = true;
                }
                if (taskManager.checkSiringa == true)
                    game.AddPontuacao(1);

            }

            game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();

            collision.gameObject.SetActive(false);
            Debug.Log("RemedioUsado");

            if ((HassCirurgia) && (taskCompleted))
            {
                if (collision.gameObject.tag == "Player")
                {
                    sirurgiaMiniGameObj.SetActive(true);
                    cirurgiaMiniGameText.SetActive(true);
                    if (pacienteInt == 1)
                    {
                        sirurgiaMiniGame.cirurgia1Started = true;
                    }
                    if (pacienteInt == 2)
                    {
                        sirurgiaMiniGame.cirurgia2Started = true;
                    }
                    if (pacienteInt == 3)
                    {
                        sirurgiaMiniGame.cirurgia3Started = true;
                    }
                    if (pacienteInt == 4)
                    {
                        sirurgiaMiniGame.cirurgia4Started = true;
                    }
                }
            }
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        taskManager = FindObjectOfType<TaskManager>();
        game = FindObjectOfType<Game>();
        sirurgiaMiniGame = FindObjectOfType<SirurgiaMinigame>();
        
    }
    private void Update()
    {
        if (taskManager.pause)
        {
            animator.Play("pause");
            return;
        }
        else
        {
            if (objective1Completed && objective2Completed)
            {
                taskCompleted = true;

            }
            if (sirurgiaMiniGame.cirurgiaCompleted)
            {
                cirurgiaCompleted = true;
            }
            else
            {
                cirurgiaCompleted = false;
            }

            if (taskCompleted) //usar os dois itens...
            {
                Debug.Log("TaskConcluida");

                if (HassCirurgia == false)
                {
                    EndTask();
                }
                if (HassCirurgia)
                {
                    if (cirurgiaCompleted)
                    {
                        cirurgiaCompleted = false;
                        EndTask();
                        game.AddPontuacao(7);
                    }

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        if ((HassCirurgia) && (taskCompleted))
        {            
            sirurgiaMiniGameObj.SetActive(true);
            cirurgiaMiniGameText.SetActive(true);
            if (pacienteInt == 1)
            {
                sirurgiaMiniGame.cirurgia1Started = true;
            }
            if (pacienteInt == 2)
            {
                sirurgiaMiniGame.cirurgia2Started = true;
            }
            if (pacienteInt == 3)
            {
                sirurgiaMiniGame.cirurgia3Started = true;
            }
            if (pacienteInt == 4)
            {
                sirurgiaMiniGame.cirurgia4Started = true;
            }
        }
        if ((HassCirurgia) && (taskCompleted))
            text.SetActive(true);
    }
    private void EndTask()
    {
        Debug.Log("EndTask");

        if (pacienteInt == 1)
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            taskManager.task1Completed = true;
            proximoPaciente1 = true;
            taskManager.pauseTimer1 = true;
            Debug.Log("Paciente1 proximo?");
            Destroy(gameObject);

            if (taskManager.timerAtual1 <= 20 && taskManager.timerAtual1 > 6)
                game.AddPontuacao(3);

            if (taskManager.timerAtual1 <= 6 && taskManager.timerAtual1 > 3)
                game.AddPontuacao(2);

            if (taskManager.timerAtual1 <= 3 && taskManager.timerAtual1 > 0)
                game.AddPontuacao(1);
            return;
        }
        if (pacienteInt == 2)
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            taskManager.task2Completed = true;
            proximoPaciente2 = true;
            taskManager.pauseTimer2 = true;
            Debug.Log("Paciente2 proximo?");
            Destroy(gameObject);

            if (taskManager.timerAtual2 <= 20 && taskManager.timerAtual2 > 6)
                game.AddPontuacao(3);

            if (taskManager.timerAtual2 <= 6 && taskManager.timerAtual2 > 3)
                game.AddPontuacao(2);

            if (taskManager.timerAtual2 <= 3 && taskManager.timerAtual2 > 0)
                game.AddPontuacao(1);
            return;
        }
        if (pacienteInt == 3)
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            taskManager.task3Completed = true;
            proximoPaciente3 = true;
            taskManager.pauseTimer3 = true;
            Debug.Log("Paciente3 proximo?");
            Destroy(gameObject);

            if (taskManager.timerAtual3 <= 20 && taskManager.timerAtual3 > 6)
                game.AddPontuacao(3);

            if (taskManager.timerAtual3 <= 6 && taskManager.timerAtual3 > 3)
                game.AddPontuacao(2);

            if (taskManager.timerAtual3 <= 3 && taskManager.timerAtual3 > 0)
                game.AddPontuacao(1);
            return;
        }
        if (pacienteInt == 4)
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            taskManager.task4Completed = true;
            proximoPaciente4 = true;
            taskManager.pauseTimer4 = true;
            Debug.Log("Paciente4 proximo?");
            Destroy(gameObject);

            if (taskManager.timerAtual4 <= 20 && taskManager.timerAtual4 > 6)
                game.AddPontuacao(3);

            if (taskManager.timerAtual4 <= 6 && taskManager.timerAtual4 > 3)
                game.AddPontuacao(2);

            if (taskManager.timerAtual4 <= 3 && taskManager.timerAtual4 > 0)
                game.AddPontuacao(1);
            return;
        }
    }
}

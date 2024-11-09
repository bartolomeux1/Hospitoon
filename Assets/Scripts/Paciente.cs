using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour
{
    public bool HassSirurgia;
    public bool SirurgiaCompleted;

    public Game game;
    public TaskManager taskManager;

    public GameObject sirurgiaMiniGame;

    public string objectTag1;
    public string objectTag2;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;

    public bool objective1Completed = false;
    public bool objective2Completed = false;
    public bool taskCompleted = false;

    public GameObject nextPaciente;
    public bool proximoPaciente1 = false;
    public bool proximoPaciente2 = false;

    public int pacienteInt = 0;
    private void OnTriggerEnter(Collider collision)
    {

            if (collision.gameObject.tag == objectTag1) //siringa...
            {
                //taskManager.objective1Completed = true;
                objective1Completed = true;
                taskObject1Ui.SetActive(false);

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkBisturi == false)
                    {

                        game.AddPontuacao(2); //adiciona 3 na pontuação
                        taskManager.checkSiringa = true;
                    }
                    if (taskManager.checkBisturi == true) 
                        game.AddPontuacao(3);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);

                Debug.Log("completed");
            }
            if (collision.gameObject.tag == objectTag2) //remedio...
            {
                Debug.Log("completed");
                //taskManager.objective2Completed = true;
                objective2Completed = true;
                taskObject2Ui.SetActive(false);

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkSiringa == false)
                    {

                        game.AddPontuacao(2); //adiciona 3 na pontuação
                        taskManager.checkBisturi = true;
                    }
                    if (taskManager.checkSiringa == true)
                        game.AddPontuacao(3);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);
                Debug.Log("completed");
            }
            if (HassSirurgia)
            {
                if (collision.gameObject.tag == "Player")
                {
                    sirurgiaMiniGame.SetActive(true);
                }
                else
                    sirurgiaMiniGame.SetActive(false);
            }
    }
    private void Update()
    {
        if (objective1Completed && objective2Completed) //usar os dois itens...
        {
            taskCompleted = true;

            if (HassSirurgia == false)
            {
                if (pacienteInt == 1)
                {
                    taskManager.task1Completed = true;
                    proximoPaciente1 = true;
                    taskManager.pauseTimer1 = true;
                }
                if (pacienteInt == 2)
                {
                    taskManager.task2Completed = true;
                    proximoPaciente2 = true;
                    taskManager.pauseTimer2 = true;
                }
                Destroy(gameObject);
            }
            if (HassSirurgia)
            {
                if (SirurgiaCompleted)
                {
                    if (pacienteInt == 1)
                    {
                        taskManager.task1Completed = true;
                        proximoPaciente1 = true;
                        taskManager.pauseTimer1 = true;
                    }
                    if (pacienteInt == 2)
                    {
                        taskManager.task2Completed = true;
                        proximoPaciente2 = true;
                        taskManager.pauseTimer2 = true;
                    }
                    Destroy(gameObject);
                }
            }



        }

    }
    /*public void NewPaciente()
    {
        taskManager.paciente1 = nextPaciente;
        Debug.Log("NewPaciente");
    }*/
}

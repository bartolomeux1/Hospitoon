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
            if (pacienteInt == 1)
            {
                if (taskManager.timerAtual1 > 10 && taskManager.timerAtual1 < 12)
                    taskManager.timerAtual1 = 12;
                else
                    taskManager.timerAtual1 += 2;
            }
            if (pacienteInt == 2)
            {
                if (taskManager.timerAtual2 > 10 && taskManager.timerAtual2 < 12)
                    taskManager.timerAtual2 = 12;
                else
                    taskManager.timerAtual2 += 2;
            }
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

            Debug.Log("SiringaUsada");
        }
        if (collision.gameObject.tag == objectTag2) //remedio...
        {

            //taskManager.objective2Completed = true;
            objective2Completed = true;
            taskObject2Ui.SetActive(false);

            if (pacienteInt == 1)
            {
                if (taskManager.timerAtual1 > 10 && taskManager.timerAtual1 < 12)
                    taskManager.timerAtual1 = 12;
                else
                    taskManager.timerAtual1 += 2;
            }
            if (pacienteInt == 2)
            {
                if (taskManager.timerAtual2 > 10 && taskManager.timerAtual2 < 12)
                    taskManager.timerAtual2 = 12;
                else
                    taskManager.timerAtual2 += 2;
            }
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
            Debug.Log("RemedioUsado");
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
            Debug.Log("TaskConcluida");

            if (HassSirurgia == false)
            {
                if (pacienteInt == 1)
                {
                    DestruirObjeto("Paciente(Clone)");
                    taskManager.task1Completed = true;
                    proximoPaciente1 = true;
                    taskManager.pauseTimer1 = true;
                    Debug.Log("Paciente1 proximo?");
                    Destroy(gameObject);
                    return;
                }
                else if (pacienteInt == 2)
                {
                    DestruirObjeto("Paciente2(Clone)");
                    taskManager.task2Completed = true;
                    proximoPaciente2 = true;
                    taskManager.pauseTimer2 = true;
                    Debug.Log("Paciente2 proximo?");
                    Destroy(gameObject);
                    return;
                }

            }
        }

    }

    public void DestruirObjeto(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        if (obj != null)
            Destroy(obj);
    }
    /*public void NewPaciente()
    {
        taskManager.paciente1 = nextPaciente;
        Debug.Log("NewPaciente");
    }*/
}

using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour
{
    public Game game;
    public TaskManager taskManager;

    public string objectTag1;
    public string objectTag2;

    public bool objective1Completed = false;
    public bool objective2Completed = false;

    public bool taskCompleted = false;

    public GameObject nextPaciente;
    public GameObject pacienteSpawn;

    public int paciente = 1;
    private void OnTriggerEnter(Collider collision)
    {

        if (paciente == 1)
        {
            if (collision.gameObject.tag == objectTag1)
            {
                taskManager.objective1Completed = true;
                objective1Completed = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkBisturi == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontua��o
                        taskManager.checkSiringa = true;
                    }
                    if (taskManager.checkBisturi == true) 
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);

                Debug.Log("completed");
            }
            if (collision.gameObject.tag == objectTag2)
            {
                Debug.Log("completed");
                taskManager.objective2Completed = true;
                objective2Completed = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkSiringa == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontua��o
                        taskManager.checkBisturi = true;
                    }
                    if (taskManager.checkSiringa == true)
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);
                Debug.Log("completed");
            }
        }
        if (paciente == 2)
        {
            if (collision.gameObject.tag == objectTag1)
            {
                taskManager.objective1Completed2 = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkBisturi == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontua��o
                        taskManager.checkSiringa = true;
                    }
                    if (taskManager.checkBisturi == true)
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);
                Debug.Log("completed");
            }
            if (collision.gameObject.tag == objectTag2)
            {
                taskManager.objective2Completed2 = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkSiringa == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontua��o
                        taskManager.checkBisturi = true;
                    }
                    if (taskManager.checkSiringa == true)
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);
                Debug.Log("completed");
            }
        }
    }
    /*private void Update()
    {
        if(objective1Completed && objective2Completed)
        {
            Invoke("NextTask", 3);
            Destroy(gameObject);
            

        }
    }
    public void NextTask()
    {
        Instantiate(nextPaciente, pacienteSpawn.transform.position, pacienteSpawn.transform.rotation);
        //taskUi1.SetActive(true);

        game.isTimerRunning = true;
    }*/
}

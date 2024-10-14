using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour
{
    public Game game;
    public TaskManager taskManager;


    public int paciente = 1;
    private void OnTriggerEnter(Collider collision)
    {

        if (paciente == 1)
        {
            if (collision.gameObject.tag == "SiringaMao")
            {
                taskManager.objective1Completed = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkBisturi == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
                        taskManager.checkSiringa = true;
                    }
                    if (taskManager.checkBisturi == true) 
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);

                Debug.Log("completed");
            }
            if (collision.gameObject.tag == "BisturiMao")
            {
                Debug.Log("completed");
                taskManager.objective2Completed = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkSiringa == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
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
            if (collision.gameObject.tag == "SiringaMao")
            {
                taskManager.objective1Completed2 = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkBisturi == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
                        taskManager.checkSiringa = true;
                    }
                    if (taskManager.checkBisturi == true)
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);
                Debug.Log("completed");
            }
            if (collision.gameObject.tag == "BisturiMao")
            {
                taskManager.objective2Completed2 = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(5); //adiciona o tempo ao interagir com paciente
                    if (taskManager.checkSiringa == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
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
}

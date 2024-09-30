using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour
{
    public Game game;


    public int paciente = 1;
    private void OnTriggerEnter(Collider collision)
    {

        if (paciente == 1)
        {
            if (collision.gameObject.tag == "SiringaMao")
            {
                game.objective1Completed = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(); //adiciona o tempo ao interagir com paciente
                    if (game.checkBisturi == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
                        game.checkSiringa = true;
                    }
                    if (game.checkBisturi == true) 
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);

                Debug.Log("completed");
            }
            if (collision.gameObject.tag == "BisturiMao")
            {
                game.objective2Completed = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(); //adiciona o tempo ao interagir com paciente
                    if (game.checkSiringa == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
                        game.checkBisturi = true;
                    }
                    if (game.checkSiringa == true)
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
                game.objective1Completed2 = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(); //adiciona o tempo ao interagir com paciente
                    if (game.checkBisturi == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
                        game.checkSiringa = true;
                    }
                    if (game.checkBisturi == true)
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);
                Debug.Log("completed");
            }
            if (collision.gameObject.tag == "BisturiMao")
            {
                game.objective2Completed2 = true;

                if (game.podeAddTimer == true)
                {
                    game.AddTimer(); //adiciona o tempo ao interagir com paciente
                    if (game.checkSiringa == false)
                    {

                        game.AddPontuacao(3); //adiciona 3 na pontuação
                        game.checkBisturi = true;
                    }
                    if (game.checkSiringa == true)
                        game.AddPontuacao(7);
                }

                game.podeAddTimer = false; // faz com que seja adicionado apenas uma vez

                collision.gameObject.SetActive(false);
                Debug.Log("completed");
            }
        }
    }
}

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
        
        if(paciente == 1)
        {
            if (collision.gameObject.tag == "SiringaMao")
            {
                game.objective1Completed = true;
                Debug.Log("completed");
            }
            if (collision.gameObject.tag == "BisturiMao")
            {
                game.objective2Completed = true;
                Debug.Log("completed");
            }
        }
        if(paciente == 2)
        {
            if (collision.gameObject.tag == "SiringaMao")
            {
                game.objective1Completed2 = true;
                Debug.Log("completed");
            }
            if (collision.gameObject.tag == "BisturiMao")
            {
                game.objective2Completed2 = true;
                Debug.Log("completed");
            }
        }
    }
}

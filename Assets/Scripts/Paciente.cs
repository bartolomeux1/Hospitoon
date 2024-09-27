using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour
{
    public Game game;


    private void OnTriggerEnter(Collider collision)
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
        //complete objective 1 if playe
    }
}

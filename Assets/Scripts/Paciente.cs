using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour
{
    public GameObject task1Object1;
    public GameObject task1Object2;

    public Game game;
    public Player player;

    private void Start()
    {
        GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (task1Object1.activeSelf)
            {
                game.objective1Completed = true;
                task1Object1.SetActive(false);
            }
            if (task1Object2.activeSelf)
            {
                game.objective2Completed = true;
                task1Object2.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (game.objective1Completed && game.objective2Completed)
        {
            Destroy(gameObject);
        }
    }
}

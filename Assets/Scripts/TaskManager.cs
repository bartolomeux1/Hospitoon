using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Game game;

    [Header("paciente gameobject")]

    public GameObject paciente1;
    public GameObject paciente1Spawn;
    public GameObject pacient1clone;

    public GameObject paciente2;
    public GameObject paciente2Spawn;
    public GameObject paciente2Clone;

    public GameObject newPaciente;

    [Header("tasks")]

    public GameObject taskUi1;
    public GameObject taskUi2;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;

    public GameObject task2Object1Ui;
    public GameObject task2Object2Ui;

    public int task1CompletedCouter;
    public int task2CompletedCouter;

    [Header("tasks booleans")]

    public bool objective1Completed = false;
    public bool objective2Completed = false;

    public bool objective1Completed2 = false;
    public bool objective2Completed2 = false;

    public bool task1Completed = false;
    public bool task2Completed = false;

    public bool checkSiringa;
    public bool checkBisturi;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Maca1Task1", 3);
        Invoke("Maca2Task1", 3);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Tasks();
    }
    public void Tasks()
    {
        //maca 1

        if (objective1Completed)
        {
            taskObject1Ui.SetActive(false);

        }
        else taskObject1Ui.SetActive(true);

        if (objective2Completed)
        {
            taskObject2Ui.SetActive(false);

        }
        else taskObject2Ui.SetActive(true);

        if (objective1Completed && objective2Completed)
        {
            task1Completed = true;

            taskUi1.SetActive(false);

        }
        if (task1Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            Invoke("Maca1Task1", 3);
            objective1Completed = false;
            objective2Completed = false;
            Destroy(pacient1clone);
            task1Completed = false;
            task1CompletedCouter++;

        }

        //maca 2

        if (objective1Completed2)
        {
            task2Object1Ui.SetActive(false);

        }
        else task2Object1Ui.SetActive(true);

        if (objective2Completed2)
        {
            task2Object2Ui.SetActive(false);


        }
        else task2Object2Ui.SetActive(true);

        if (objective1Completed2 && objective2Completed2)
        {
            task2Completed = true;
            taskUi2.SetActive(false);
        }
        if (task2Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            Invoke("Maca2Task1", 3);
            objective1Completed2 = false;
            objective2Completed2 = false;
            Destroy(paciente2Clone);
            task2Completed = false;
            task2CompletedCouter++;
        }
        if (task1CompletedCouter > 3)
        {
            NewPaciente();
            task1CompletedCouter = 0;
        }
    }
    public void Maca1Task1()
    {
        Instantiate(paciente1, paciente1Spawn.transform.position, paciente1Spawn.transform.rotation);
        taskUi1.SetActive(true);

        game.isTimerRunning = true;

        pacient1clone = GameObject.Find("Paciente(Clone)");
    }
    public void Maca2Task1()
    {
        Instantiate(paciente2, paciente2Spawn.transform.position, paciente2Spawn.transform.rotation);
        taskUi2.SetActive(true);

        paciente2Clone = GameObject.Find("Paciente2(Clone)");
    }

    public void NewPaciente()
    {
        paciente1 = newPaciente;
        Debug.Log("NewPaciente");
    }
}

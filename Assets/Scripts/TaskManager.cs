using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Game game;

    [Header("paciente gameobject")]

    public GameObject paciente1;
    public GameObject paciente1Spawn;

    public GameObject paciente2;
    public GameObject paciente2Spawn;
    
    public GameObject newPaciente;
    public GameObject newPaciente2;

    private GameObject paciente1Backup;
    private GameObject Paciente2Backup;

    [Header("tasks")]

    public GameObject taskUi1;
    public GameObject taskUi2;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;

    public GameObject task2Object1Ui;
    public GameObject task2Object2Ui;

    public int task1CompletedCouter;
    public int task2CompletedCouter;

    public int task1CanceledCouter;
    public int task2CanceledCouter;

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
        paciente1Backup = paciente1;
        Paciente2Backup = paciente2;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Tasks();
    }
    public void Tasks()
    {
        //maca 1
        if (task1Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            Invoke("Maca1Task1", 3);
            task1Completed = false;
            task1CompletedCouter++;

        }
        //maca 2
        if (task2Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            Invoke("Maca2Task1", 3);
            task2Completed = false;
            task2CompletedCouter++;
        }
        if (task1CompletedCouter > 3)
        {
            NewPaciente1();
            task1CanceledCouter++;
        }
        if(task2CompletedCouter > 3) 
        { 
            NewPaciente2(); 
            task2CanceledCouter++;
        }
        if((task1CompletedCouter == 1) && (paciente1 == newPaciente)) { BackuPaciente1(); }
        if((task2CompletedCouter == 1) && (paciente2 == newPaciente2)) { BackuPaciente2();}
    }
    public void Maca1Task1()
    {
        Debug.Log("paciente 1 spawned");
        Instantiate(paciente1, paciente1Spawn.transform.position, paciente1Spawn.transform.rotation);

        game.isTimerRunning = true;
    }
    public void Maca2Task1()
    {
        Instantiate(paciente2, paciente2Spawn.transform.position, paciente2Spawn.transform.rotation);
    }

    public void NewPaciente1()
    {
        paciente1 = newPaciente;
        Debug.Log("NewPaciente");
        task1CompletedCouter = 0;
    }
    public void NewPaciente2()
    {
        paciente2 = newPaciente2;
        task2CompletedCouter = 0; 
    }
    public void BackuPaciente1()
    {
        paciente1 = paciente1Backup;
    }
    public void BackuPaciente2()
    {
        paciente2 = Paciente2Backup;
    }
}

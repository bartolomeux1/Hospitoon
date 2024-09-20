using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject paciente1;   
    public GameObject pacienteSpawn;
    public GameObject TaskUi1;

    public GameObject taskObject1;
    public GameObject taskObject2;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;

    public bool objective1Completed = false;
    public bool objective2Completed = false;

    bool task1Completed = false;

    public SphereCollider macaCollider;
    // Start is called before the first frame update
    void Start()
    {
        // start task 1 after 5 seconds
        Invoke("Task1", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Task1()
    {
        Instantiate(paciente1, pacienteSpawn.transform.position, pacienteSpawn.transform.rotation);
        TaskUi1.SetActive(true);

        if (objective1Completed && objective2Completed)
        {
            task1Completed = true;
            TaskUi1.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (taskObject1.activeSelf)
            {
                taskObject1.SetActive(false);
                taskObject1Ui.SetActive(false);
                objective1Completed = true;
            }
            if (taskObject2.activeSelf)
            {
                taskObject2.SetActive(false);
                taskObject2Ui.SetActive(false);
                objective2Completed = true;
            }
        }
    }



}

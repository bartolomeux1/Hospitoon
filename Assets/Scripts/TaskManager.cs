
using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Game game;
    public Paciente paciente;

    [Header("paciente gameobject")]

    public GameObject paciente1;
    public GameObject paciente1Spawn;

    public GameObject paciente2;
    public GameObject paciente2Spawn;

    public GameObject paciente3;
    public GameObject paciente3Spawn;

    public GameObject paciente4;
    public GameObject paciente4Spawn;

    public GameObject newPaciente;
    public GameObject newPaciente2;
    public GameObject newPaciente3;
    public GameObject newPaciente4;

    public GameObject paciente1Backup;
    public GameObject Paciente2Backup;
    public GameObject paciente3Backup;
    public GameObject Paciente4Backup;

    private GameObject paciente1Clone; // Referência para o clone de paciente1
    private GameObject paciente2Clone; // Referência para o clone de paciente2
    private GameObject paciente3Clone;
    private GameObject paciente4Clone;

    [Header("tasks")]

    public GameObject taskUi1;
    public GameObject taskUi2;
    public GameObject taskUi3;
    public GameObject taskUi4;

    public GameObject taskObject1Ui;
    public GameObject taskObject2Ui;

    public GameObject task2Object1Ui;
    public GameObject task2Object2Ui;

    public GameObject task3Object1Ui;
    public GameObject task3Obkect2Ui;

    public GameObject task4Obkect1Ui;
    public GameObject task4Obkect2Ui;

    public int task1CompletedCouter;
    public int task2CompletedCouter;
    public int task3CompletedCouter;
    public int task4CompletedCouter;

    public int task1CanceledCouter;
    public int task2CanceledCouter;
    public int task3CanceledCouter;
    public int task4CanceledCouter;


    [Header("tasks booleans")]

    public bool objective1Completed = false;
    public bool objective2Completed = false;

    public bool objective1Completed2 = false;
    public bool objective2Completed2 = false;

    public bool objective1Completed3 = false;
    public bool objective2Completed3 = false;

    public bool objective1Completed4 = false;
    public bool objective2Completed4 = false;

    public bool paciente4Proximo = false;

    public bool task1Completed = false;
    public bool task2Completed = false;
    public bool task3Completed = false;
    public bool task4Completed = false;

    public bool checkSiringa;
    public bool checkBisturi;

    public bool pauseTimer1 = false;
    public bool pauseTimer2 = false;
    public bool pauseTimer3 = false;
    public bool pauseTimer4 = false;
    private float timerSpawn;
    public float timerAtual1;
    public float timerAtual2;
    public float timerAtual3;
    public float timerAtual4;
    public float timerMaca1 = 12;
    public float timerMaca2 = 12;
    public float timerMaca3 = 12;
    public float timerMaca4 = 12;
    public Sprite[] emojis;
    public Image emojisImage;
    public Image emojisImage2;
    public Image emojisImage3;
    public Image emojisImage4;
    public Slider timerSlider;
    public Image fillImage;
    public Slider timerSlider2;
    public Image fillImage2;
    public Slider timerSlider3;
    public Image fillImage3;
    public Slider timerSlider4;
    public Image fillImage4;

    public bool pause = false;
    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartMaca1Task", RpcTarget.AllBuffered);
            photonView.RPC("StartMaca2Task", RpcTarget.AllBuffered);
            photonView.RPC("StartMaca3Task", RpcTarget.AllBuffered);
            photonView.RPC("StartMaca4Task", RpcTarget.AllBuffered);
        }


       fillImage  = timerSlider. fillRect.GetComponent<Image>();
       fillImage2 = timerSlider2.fillRect.GetComponent<Image>();
       fillImage3 = timerSlider3.fillRect.GetComponent<Image>();
       fillImage4 = timerSlider4.fillRect.GetComponent<Image>();

    
        paciente1Backup = paciente1;
        Paciente2Backup = paciente2;
        paciente3Backup = paciente3;
        Paciente4Backup = paciente4;
    }

    public void Update()
    {
        if (pause)
            return;
        else
        {
            if (!task1Completed)
                TimerMaca1();
            if (!task2Completed)
                TimerMaca2();
            if (!task3Completed)
                TimerMaca3();
            if (!task4Completed)
                TimerMaca4();

            if(timerAtual1 <=0)
                InviFill();
            if(timerAtual2 <=0)
                InviFill2();
            if (timerAtual3 <= 0)
                InviFill3();
            if (timerAtual4 <= 0)
                InviFill4();
            if(timerAtual1 >=0)
                ShowFill();
            if(timerAtual2 >=0)
                ShowFill2();
            if (timerAtual3 >= 0)
                ShowFill3();
            if (timerAtual4 >= 0)
                ShowFill4();

            if (paciente.proximoPaciente1 && PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("StartMaca1Task", RpcTarget.All);
                task1Completed = false;
                paciente.proximoPaciente1 = false;
            }

            if (paciente.proximoPaciente2 && PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("StartMaca2Task", RpcTarget.All);
                task2Completed = false;
                paciente.proximoPaciente2 = false;
            }
            if (paciente.proximoPaciente3 && PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("StartMaca3Task", RpcTarget.All);
                task3Completed = false;
                paciente.proximoPaciente3 = false;
            }
            if ((paciente.proximoPaciente4 && PhotonNetwork.IsMasterClient) || (paciente4Proximo && PhotonNetwork.IsMasterClient))
            {
                photonView.RPC("StartMaca4Task", RpcTarget.All);
                task4Completed = false;
                paciente.proximoPaciente4 = false;
                Debug.Log("Chamando StartMaca4Task");
            }
        }
    }
    [PunRPC]
    void StartMaca1Task()
    {
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + Random.Range(1, 3); // espera de 5/15 segundos

        StartCoroutine(WaitAndExecuteTask(taskStartTime));
    }
    [PunRPC]
    void StartMaca2Task()
    {
        //Debug.Log("Chamando StartMaca2Task");
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + Random.Range(1, 3); // espera de 3/10 segundos

        StartCoroutine(WaitAndExecuteTask2(taskStartTime));
    }
    [PunRPC]
    void StartMaca3Task()
    {
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + Random.Range(1, 3);

        StartCoroutine(WaitAndExecuteTask3(taskStartTime));
    }
    [PunRPC]
    void StartMaca4Task()
    {
        Debug.Log("Chamando StartMaca4Task");
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + Random.Range(2, 4);
        paciente4Proximo = false;

        StartCoroutine(WaitAndExecuteTask4(taskStartTime));
    }
    IEnumerator WaitAndExecuteTask(double startTime)
    {
        while (PhotonNetwork.Time < startTime)
        {
            yield return null; // espera até o tempo de início
        }

        Maca1Task1();
    }
    IEnumerator WaitAndExecuteTask2(double startTime)
    {
        Debug.Log("Iniciando a espera em WaitAndExecuteTask2");
        while (PhotonNetwork.Time < startTime)
        {
            yield return null; // espera até o tempo de início
        }
        Debug.Log("Executando Maca2Task1");
        Maca2Task1();
    }
    IEnumerator WaitAndExecuteTask3(double startTime)
    {
        while (PhotonNetwork.Time < startTime)
        {
            yield return null;
        }

        Maca3Task1();
    }
    IEnumerator WaitAndExecuteTask4(double startTime)
    {
        while (PhotonNetwork.Time < startTime) yield return null;
        Maca4Task1();
    }
    private void TimerMaca1()
    {
        if(pause)
            return;
        else
        {
            if (!pauseTimer1)
            {
                if (timerAtual1 > 0)
                {
                    
                    timerAtual1 -= Time.deltaTime;
                    timerSlider.value = (float)timerAtual1;

                    UpdateEmoji1();

                    // Quando o timer chega a 0, destrói o objeto em ambos os clientes
                    if (timerAtual1 <= 0)
                    {
                        
                        photonView.RPC("DestroyPaciente1", RpcTarget.AllBuffered);
                    }
                }
            }
            if (pauseTimer1)
                timerAtual1 = timerAtual1;
        }
    }
    private void TimerMaca2()
    {
        if (pause)
            return;
        else
        {
            if (!pauseTimer2)
            {
                if (timerAtual2 > 0)
                {
                    ShowFill2();

                    timerAtual2 -= 1 * Time.deltaTime;
                    timerSlider2.value = (float)timerAtual2;

                    UpdateEmoji2();


                    // Quando o timer chega a 0, destrói o objeto
                    if (timerAtual2 <= 0)
                    {
                        InviFill2();
                        photonView.RPC("DestroyPaciente2", RpcTarget.AllBuffered);
                    }
                }
                if (pauseTimer2)
                    timerAtual2 = timerAtual2;
            }
        }
    }
    private void TimerMaca3()
    {
        if (pause) return;
        else
        {
            if (!pauseTimer3)
            {
                if (timerAtual3 > 0)
                {
                    ShowFill3();
                    timerAtual3 -=1 * Time.deltaTime;
                    timerSlider3.value = (float)timerAtual3;

                    UpdateEmoji3();

                    if (timerAtual3 <= 0)
                    {
                        InviFill3();
                        photonView. RPC("DestroyPaciente3",RpcTarget.AllBuffered);
                    }
                }
                if (pauseTimer3)
                    timerAtual3 = timerAtual3;
            }
        }
    }
    private void TimerMaca4()
    {
        if (pause) return;
        else
        {
            if (!pauseTimer4)
            {
                if (timerAtual4 > 0)
                {
                    ShowFill4();
                    timerAtual4 -= 1 * Time.deltaTime;
                    timerSlider4.value = (float)timerAtual4;

                    UpdateEmoji4();

                    if (timerAtual4 <= 0)
                    {
                        InviFill3();
                        photonView.RPC("DestroyPaciente4", RpcTarget.AllBuffered);
                    }
                }
                if (pauseTimer4)
                    timerAtual4 = timerAtual4;
            }
        }
    }
    [PunRPC]
    void DestroyPaciente1()
    {
        if (paciente1Clone != null)
        {
            Destroy(paciente1Clone);
            paciente1Clone = null;
            paciente.proximoPaciente1 = true;

            if (timerAtual1 <= 0)
                game.SubPontuacao(2);
                
        }
    }
    [PunRPC]
    void DestroyPaciente2()
    {
        if (paciente2Clone != null)
        {
            Destroy(paciente2Clone);
            paciente2Clone = null;
            paciente.proximoPaciente2 = true;

           if (timerAtual2 <= 0)
                game.SubPontuacao(2);

        }
    }
    [PunRPC]
    void DestroyPaciente3()
    {
        if (paciente3Clone != null)
        {
            Destroy(paciente3Clone);
            paciente3Clone = null;
            paciente.proximoPaciente3 = true;

            if(timerAtual3 <= 0)
                Debug.Log("Subtraindo 2 pontos");   
                game.SubPontuacao(2);
        }
    }
    [PunRPC]
    void DestroyPaciente4()
    {
        if (paciente4Clone != null)
        {
            Destroy(paciente4Clone);
            paciente4Clone = null;
            paciente.proximoPaciente4 = true;

            if (timerAtual4 <= 0)
                game.SubPontuacao(2);
        }
    }
    private void UpdateEmoji1()
    {
        if (pause)
            return;
        else
        {
            // Atualiza o emoji baseado no tempo atual
            if (timerAtual1 <= 9 && timerAtual1 > 6)
                emojisImage.sprite = emojis[1];

            else if (timerAtual1 <= 6 && timerAtual1 > 3)
                emojisImage.sprite = emojis[2];

            else if (timerAtual1 <= 3 && timerAtual1 > 0)
                emojisImage.sprite = emojis[3];

            else if (timerAtual1 <= 0)
                emojisImage.sprite = emojis[4];
        }
    }
    private void UpdateEmoji2()
    {
        if (pause)
            return;
        else
        {
            // Atualiza o emoji baseado no tempo atual
            if (timerAtual2 <= 9 && timerAtual2 > 6)
                emojisImage2.sprite = emojis[1];

            else if (timerAtual2 <= 6 && timerAtual2 > 3)
                emojisImage2.sprite = emojis[2];

            else if (timerAtual2 <= 3 && timerAtual2 > 0)
                emojisImage2.sprite = emojis[3];

            else if (timerAtual2 <= 0)
                emojisImage2.sprite = emojis[4];
        }
    }
    private void UpdateEmoji3()
    {
        if (pause)
            return;
        else
        {
            // Atualiza o emoji baseado no tempo atual
            if (timerAtual3 <= 9 && timerAtual3 > 6)
                emojisImage3.sprite = emojis[1];

            else if (timerAtual3 <= 6 && timerAtual3 > 3)
                emojisImage3.sprite = emojis[3];

            else if (timerAtual3 <= 3 && timerAtual3 > 0)
                emojisImage3.sprite = emojis[3];

            else if (timerAtual3 <= 0)
                emojisImage3.sprite = emojis[4];
        }
    }
    void UpdateEmoji4()
    {
        // Atualiza o emoji baseado no tempo atual
        if (timerAtual4 <= 9 && timerAtual4 > 6)
            emojisImage4.sprite = emojis[1];

        else if (timerAtual4 <= 6 && timerAtual4 > 3)
            emojisImage4.sprite = emojis[3];

        else if (timerAtual3 <= 3 && timerAtual4 > 0)
            emojisImage4.sprite = emojis[3];

        else if (timerAtual4 <= 0)
            emojisImage4.sprite = emojis[4];
    }
    public void Tasks() //game usa pra rodar tasks.
    {
        //maca 1
        if (task1Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            paciente.proximoPaciente1 = true;
            task1Completed = false;
            task1CompletedCouter++;

        }
        //maca 2
        if (task2Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            paciente.proximoPaciente2 = true;
            task2Completed = false;
            task2CompletedCouter++;
        }
        // maca3
        if (task3Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            paciente.proximoPaciente3 = true;
            task3Completed = false;
            task3CompletedCouter++;
        }
        // maca4
        if (task4Completed)
        {
            checkSiringa = false;
            checkBisturi = false;
            paciente.proximoPaciente3 = true;
            task4Completed = false;
            task4CompletedCouter++;
        }

        if (task1CompletedCouter > 3)
        {
            NewPaciente1();
            //task1CanceledCouter++;
            timerMaca1 = +18;
        }
        if (task2CompletedCouter > 3)
        {
            NewPaciente2();
            //task2CanceledCouter++;
            timerMaca2 = +18;
        }
        if (task3CompletedCouter > 3)
        {
            NewPaciente3();
            //task3CompletedCouter++;
            timerMaca3 = +18;
        }
        if (task4CompletedCouter > 3)
        {
            NewPaciente4();
            //task4CompletedCouter++;
            timerMaca4 = +18;
        }
        if ((task1CompletedCouter > 0)) 
        { 
            BackuPaciente1();
            timerMaca1 = 12;
        }
        if ((task2CompletedCouter > 0))
        {
            BackuPaciente2();
            timerMaca2 = 12;
        }
        if (task3CompletedCouter > 0)
        {
            BackuPaciente3();
            timerMaca3 = 12;
        }
        if (task1CompletedCouter > 0)
        {
            BackuPaciente4();
            timerMaca4 = 12;
        }
    }
    public void Maca1Task1()
    {

        if (paciente1Clone != null)
            Destroy(paciente1Clone);

        paciente1Clone = Instantiate(paciente1, paciente1Spawn.transform.position, paciente1Spawn.transform.rotation);
        
        //Instantiate(paciente1, paciente1Spawn.transform.position, paciente1Spawn.transform.rotation);

        game.isTimerRunning = true;
        pauseTimer1 = false;

        timerAtual1 = timerMaca1;
        timerSlider.maxValue = timerMaca1;
        timerSlider.value = timerAtual1;
        emojisImage.sprite = emojis[0];

    }
    public void Maca2Task1()
    {
        Debug.Log("Instanciando Maca2Task1");
        if (paciente2Clone != null)
            Destroy(paciente2Clone);


        paciente2Clone = Instantiate(paciente2, paciente2Spawn.transform.position, paciente2Spawn.transform.rotation);

        //Instantiate(paciente2, paciente2Spawn.transform.position, paciente2Spawn.transform.rotation);

        game.isTimerRunning = true;
        pauseTimer2 = false;

        timerAtual2 = timerMaca2;
        timerSlider2.maxValue = timerMaca2;
        timerSlider2.value = timerAtual2;
        emojisImage2.sprite = emojis[0];


    }

    public void Maca3Task1()
    {
        if (paciente3Clone != null)
            Destroy(paciente3Clone);

        paciente3Clone = Instantiate(paciente3, paciente3Spawn.transform.position, paciente3Spawn.transform.rotation);

        game.isTimerRunning = true;
        pauseTimer3 = false;

        timerAtual3 = timerMaca3;
        timerSlider3.maxValue = timerMaca3;
        timerSlider3.value = timerAtual3;
        emojisImage3.sprite= emojis[0];

    }
    public void Maca4Task1()
    {
        if (paciente4Clone != null)
            Destroy(paciente4Clone);

        paciente4Clone = Instantiate(paciente4, paciente4Spawn.transform.position, paciente4Spawn.transform.rotation);

        game.isTimerRunning = true;
        pauseTimer4 = false;

        timerAtual4 = timerMaca4;
        timerSlider4.maxValue = timerMaca4;
        timerSlider4.value = timerAtual4;
        emojisImage4.sprite = emojis[0];
    }
    public void InviFill()
    {
        if (paciente1)
            fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, 0f);
    }
    public void InviFill2()
    {
        if (paciente2)
            fillImage2.color = new Color(fillImage2.color.r, fillImage2.color.g, fillImage2.color.b, 0f);
    }
    public void InviFill3()
    {
        if (paciente3)
            fillImage3.color = new Color(fillImage3.color.r, fillImage3.color.g,fillImage3.color.b, 0f);
    }
    public void InviFill4()
    {
        if (paciente4)
            fillImage4.color = new Color(fillImage4.color.r, fillImage4.color.g, fillImage4.color.b, 0f);
    }
    public void ShowFill()
    {
        if (paciente1)
            fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, 255f);
    }
    public void ShowFill2()
    {
        if (paciente2)
            fillImage2.color = new Color(fillImage2.color.r, fillImage2.color.g, fillImage2.color.b, 255f);
    }
    public void ShowFill3()
    {
        if (paciente3)
            fillImage3.color = new Color(fillImage3.color.r, fillImage3.color.g, fillImage3.color.b, 255f);
    }
    public void ShowFill4()
    {
        if (paciente4)
            fillImage4.color = new Color(fillImage4.color.r, fillImage4.color.g, fillImage4.color.b, 255f);
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
    public void NewPaciente3()
    {
        paciente3 = newPaciente3;
        Debug.Log("NewPaciente3");
        task3CompletedCouter = 0;
    }
    public void NewPaciente4()
    {
        paciente4 = newPaciente4;
        task4CompletedCouter = 0;
    }
    public void BackuPaciente1()
    {
        paciente1 = paciente1Backup;
        Debug.Log("BackupPaciente1");
    }
    public void BackuPaciente2()
    {
        paciente2 = Paciente2Backup;
    }
    public void BackuPaciente3()
    {
        paciente3 = paciente3Backup;
    }
    public void BackuPaciente4()
    {
        paciente4 = Paciente4Backup;
    }
}

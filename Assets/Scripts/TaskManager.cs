
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
    
    public GameObject newPaciente;
    public GameObject newPaciente2;

    private GameObject paciente1Backup;
    private GameObject Paciente2Backup;


    private GameObject paciente1Clone; // Referência para o clone de paciente1
    private GameObject paciente2Clone; // Referência para o clone de paciente2

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

    public bool pauseTimer1 = false;
    public bool pauseTimer2 = false;
    private float timerSpawn;
    private float timerAtual1;
    private float timerAtual2;
    public float timerMaca1 = 12;
    public float timerMaca2 = 12;
    public Sprite[] emojis;
    public Image emojisImage;
    public Image emojisImage2;
    public Slider timerSlider;
    public Slider timerSlider2;

   public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartMaca2Task", RpcTarget.AllBuffered);
            photonView.RPC("StartMaca1Task", RpcTarget.AllBuffered);
        }
        paciente1Backup = paciente1;
        Paciente2Backup = paciente2;
    }

    public void Update()
    {
        if (!task1Completed)
            TimerMaca1();
        if ( !task2Completed)
            TimerMaca2();

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
    }
    [PunRPC]
    void StartMaca1Task()
    {
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + 2.0; // espera de 2 segundos

        StartCoroutine(WaitAndExecuteTask(taskStartTime));
    }
    [PunRPC]
    void StartMaca2Task()
    {
        Debug.Log("Chamando StartMaca2Task");
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + 3.0; // espera de 3 segundos

        StartCoroutine(WaitAndExecuteTask2(taskStartTime));
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
    private void TimerMaca1()
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
    }
    [PunRPC]
    void DestroyPaciente1()
    {
        if (paciente1Clone != null)
        {
            Destroy(paciente1Clone);
            paciente1Clone = null;
            paciente.proximoPaciente1 = true;
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
        }
    }
    private void TimerMaca2()
    {
        if (!pauseTimer2)
        {
            if (timerAtual2 > 0)
            {
                timerAtual2 -= 1 * Time.deltaTime;
                timerSlider2.value = timerAtual2;

                UpdateEmoji2();


                // Quando o timer chega a 0, destrói o objeto
                if (timerAtual2 <= 0)
                {
                    photonView.RPC("DestroyPaciente2", RpcTarget.AllBuffered);
                }
            }
            if (pauseTimer2)
                timerAtual2 = timerAtual2;
        }

    }
    private void UpdateEmoji1()
    {
        // Atualiza o emoji baseado no tempo atual
        if (timerAtual1 <= 9 && timerAtual1 > 6)
            emojisImage.sprite = emojis[1];
        
        else if (timerAtual1 <= 6 && timerAtual1 > 3)
            emojisImage.sprite = emojis[2];
       
        else if (timerAtual1 <= 3 && timerAtual1 > 0)
            emojisImage.sprite = emojis[3];
        
        else if (timerAtual1 <=0)
            emojisImage.sprite = emojis[4];
    }
    private void UpdateEmoji2()
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
        if((task2CompletedCouter == 1) && (paciente2 == newPaciente2)) { BackuPaciente2(); }
    }
    public void Maca1Task1()
    {

        if (paciente2Clone != null)
            Destroy(paciente2Clone);

        paciente1Clone = Instantiate(paciente1, paciente1Spawn.transform.position, paciente1Spawn.transform.rotation);
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
        pauseTimer2 = false;

        timerAtual2 = timerMaca2;
        timerSlider2.maxValue = timerMaca2;
        timerSlider2.value = timerAtual2;
        emojisImage2.sprite = emojis[0];

        
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

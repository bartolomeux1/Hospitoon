
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

    public GameObject paciente1Backup;
    public GameObject Paciente2Backup;


    private GameObject paciente1Clone; // Refer�ncia para o clone de paciente1
    private GameObject paciente2Clone; // Refer�ncia para o clone de paciente2

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
    public float timerAtual1;
    public float timerAtual2;
    public float timerMaca1 = 12;
    public float timerMaca2 = 12;
    public Sprite[] emojis;
    public Image emojisImage;
    public Image emojisImage2;
    public Slider timerSlider;
    public Image fillImage;
    public Slider timerSlider2;
    public Image fillImage2;

    public bool pause = false;
   public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartMaca2Task", RpcTarget.AllBuffered);
            photonView.RPC("StartMaca1Task", RpcTarget.AllBuffered);
        }


       fillImage = timerSlider.fillRect.GetComponent<Image>();
       fillImage2 = timerSlider2.fillRect.GetComponent<Image>();

    
        paciente1Backup = paciente1;
        Paciente2Backup = paciente2;
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

            if(timerAtual1 <=0)
                InviFill();
            if(timerAtual2 <=0)
                InviFill2();
            if(timerAtual1 >=0)
                ShowFill();
            if(timerAtual2 >=0)
                ShowFill2();
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
    }
    [PunRPC]
    void StartMaca1Task()
    {
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + Random.Range(1,3); // espera de 1/3 segundos

        StartCoroutine(WaitAndExecuteTask(taskStartTime));
    }
    [PunRPC]
    void StartMaca2Task()
    {
        Debug.Log("Chamando StartMaca2Task");
        double currentTime = PhotonNetwork.Time;
        double taskStartTime = currentTime + Random.Range(3, 7); ; // espera de 3/7 segundos

        StartCoroutine(WaitAndExecuteTask2(taskStartTime));
    }
    IEnumerator WaitAndExecuteTask(double startTime)
    {
        while (PhotonNetwork.Time < startTime)
        {
            yield return null; // espera at� o tempo de in�cio
        }

        Maca1Task1();
    }
    IEnumerator WaitAndExecuteTask2(double startTime)
    {
        Debug.Log("Iniciando a espera em WaitAndExecuteTask2");
        while (PhotonNetwork.Time < startTime)
        {
            yield return null; // espera at� o tempo de in�cio
        }
        Debug.Log("Executando Maca2Task1");
        Maca2Task1();
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

                    // Quando o timer chega a 0, destr�i o objeto em ambos os clientes
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
        if(pause)
            return;
        else
        {
            if (!pauseTimer2)
            {
                if (timerAtual2 > 0)
                {
                    ShowFill();
                   
                    timerAtual2 -= 1 * Time.deltaTime;
                    timerSlider2.value = (float)timerAtual2;

                    UpdateEmoji2();


                    // Quando o timer chega a 0, destr�i o objeto
                    if (timerAtual2 <= 0)
                    {
                        InviFill();
                        photonView.RPC("DestroyPaciente2", RpcTarget.AllBuffered);
                    }
                }
                if (pauseTimer2)
                    timerAtual2 = timerAtual2;
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
            timerMaca1 = +18;
        }
        if(task2CompletedCouter > 3) 
        { 
            NewPaciente2(); 
            task2CanceledCouter++;
            timerMaca2 = + 18;
        }
        if((task1CompletedCouter > 0)) 
        { 
            BackuPaciente1();
            timerMaca1 = 12;
        }
        if((task2CompletedCouter > 0)) 
        { 
            BackuPaciente2();
            timerMaca2 = 12;
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
        Debug.Log("BackupPaciente1");
    }
    public void BackuPaciente2()
    {
        paciente2 = Paciente2Backup;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JawapanSusunanController : MonoBehaviour
{
    [Header("Nama Scene")]
    public string sceneName;
    public List<Transform> questionlist;
    public bool isgamestart = false;
    public float timeRemaining = 10f;
    bool startTimer = false;
    [Header("Audios")]
    public AudioSource betul;
    public AudioSource salah;
    public AudioSource pop;

    [Header("TimerSlider")]
    public Slider timerSlider;

    [Header("References")]
    QuestionPrasekolahController soalan;
    HealthController health;

    ScoreController level;
    public bool isHealthEmpty;

    public Transform levelDisplay;
    public Transform failedDisplay;
    public Transform winDisplay;
    //public Transform nextDisplayLevel;

    public Transform btnSound;
    public Transform btnMula;
    public Transform buttonSeterusnya;
    [Header("Steps")]//utk kira markah 
    public int steps = 1;
    [Header("bilangan soalan asal")]
    public int bil;

    public int a;
    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        isHealthEmpty = !health.health.Any();
        if (isHealthEmpty)
        {
            failedDisplay.gameObject.SetActive(true);
        }
    }

    void Initialized()
    {
        sceneName = SceneManager.GetActiveScene().name;
        bil = transform.childCount;
        steps = 1;
        btnSound = GameObject.Find("btnSound").GetComponent<Transform>();
        btnMula = GameObject.Find("btnMula").GetComponent<Transform>();
        failedDisplay = GameObject.Find("FailedDIsplay").GetComponent<Transform>();
        failedDisplay.gameObject.SetActive(false);
        winDisplay = GameObject.Find("WinDisplay").GetComponent<Transform>();
        winDisplay.gameObject.SetActive(false);
        //nextDisplayLevel = GameObject.Find("WinNextDisplay").GetComponent<Transform>();
        //nextDisplayLevel.gameObject.SetActive(false);
        levelDisplay = GameObject.Find("LevelDisplay").GetComponent<Transform>();
        level = GameObject.Find("Canvas").GetComponent<ScoreController>();
        health = GameObject.Find("health").GetComponent<HealthController>();

        betul = GameObject.Find("yay").GetComponent<AudioSource>();
        salah = GameObject.Find("salah").GetComponent<AudioSource>();
        pop = GameObject.Find("pop").GetComponent<AudioSource>();
        timerSlider = GameObject.Find("Slider").GetComponent<Slider>();
        levelDisplay.gameObject.SetActive(false);

        //btnSound.gameObject.SetActive(false);
        btnMula.GetComponent<Button>().interactable = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            questionlist.Add(transform.GetChild(i));

        }
        for (int i = 0; i < transform.childCount; i++)
        {
            questionlist[i].gameObject.SetActive(false);

        }

        //

    }

    IEnumerator startingLevel()
    {
        btnMula.GetComponent<Transform>().gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        questionlist[0].gameObject.SetActive(true);

    }
    public void StartLevel()
    {
        StartCoroutine(startingLevel());
    }

    public void NextLevel()
    {
        //check the current level 
        //display next levels 
        while (a < transform.childCount)
        {

            if (!transform.GetChild(a).gameObject.activeSelf)
            {

                transform.GetChild(a).gameObject.SetActive(true);
                Destroy(transform.GetChild(a - 1).gameObject);
                a = transform.childCount;
                goto skipHint;
            }
            else
            {
                a++;
            }
        }
        a = 0;
        //check if no more level
        while (a < transform.childCount)
        {

            if (transform.GetChild(a).gameObject.activeSelf)
            {

                a++;

            }
            if (a == transform.childCount)
            {
                Debug.Log("finish");
                winDisplay.gameObject.SetActive(true);
            }
        }
    skipHint:

        a = 0;

    }
}

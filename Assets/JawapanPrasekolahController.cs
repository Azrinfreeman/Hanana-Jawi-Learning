using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class JawapanPrasekolahController : MonoBehaviour
{
    public List<Transform> questionlist;
    public bool isgamestart = false;
    public float timeRemaining = 10f;
    bool startTimer = false;

    [Header("References")]
    QuestionPrasekolahController soalan;
    HealthController health;

    ScoreController level;
    public bool isHealthEmpty;

    public Transform levelDisplay;
    public Transform failedDisplay;

    [Header("Audios")]
    public AudioSource betul;
    public AudioSource salah;
    public AudioSource pop;

    [Header("Animation")]
    public Animator jawapanAnim;

    [Header("TimerSlider")]
    public Slider timerSlider;

    [Header("Random number 1 - 3 ")]
    public int[] numbers = new int[3];

    // Start is called before the first frame update
    void Initialized()
    {
        jawapanAnim = GetComponent<Animator>();
        failedDisplay = GameObject.Find("FailedDIsplay").GetComponent<Transform>();
        failedDisplay.gameObject.SetActive(false);
        levelDisplay = GameObject.Find("LevelDisplay").GetComponent<Transform>();
        level = GameObject.Find("Canvas").GetComponent<ScoreController>();
        health = GameObject.Find("health").GetComponent<HealthController>();
        soalan = GameObject.Find("soalan").GetComponent<QuestionPrasekolahController>();
        betul = GameObject.Find("yay").GetComponent<AudioSource>();
        salah = GameObject.Find("salah").GetComponent<AudioSource>();
        pop = GameObject.Find("pop").GetComponent<AudioSource>();
        timerSlider = GameObject.Find("Slider").GetComponent<Slider>();
        levelDisplay.gameObject.SetActive(false);

        for (int i = 0; i < transform.childCount; i++)
        {
            questionlist.Add(transform.GetChild(i));
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            questionlist[i].gameObject.SetActive(false);
        }
    }

    IEnumerator ChooseAnswer()
    {
        startTimer = false;
        //iftheres time
        if (timeRemaining > 0)
        {
            //remove in list
            int t = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();

            //assign the touch selected to a variable
            GameObject currentTransform = EventSystem.current.currentSelectedGameObject.gameObject;
            //disable the buttons
            for (int i = 0; i < 3; i++)
            {
                //Debug.Log(soalan.questions[i].gameObject.GetComponent<Button>());
                soalan.questions[i].gameObject.GetComponent<Button>().enabled = false;
            }

            yield return new WaitForSeconds(1f);
            //change 3 image coilor to red
            for (int i = 0; i < soalan.questions.Count; i++)
            {
                soalan.questions[i].GetComponent<Image>().color = new Color(255, 0, 0, 255);
            }

            //check if correct
            if (currentTransform.transform.name.ToString().Equals(soalan.realAnswer))
            {
                soalan.questions[soalan.randomNumber].GetComponent<Image>().color = new Color(
                    0,
                    255,
                    0,
                    255
                );
                yield return new WaitForSeconds(1f);
                soalan.questions[soalan.randomNumber].GetComponent<Image>().color = new Color(
                    0,
                    0,
                    0,
                    0
                );
                currentTransform
                    .transform
                    .GetChild(1)
                    .GetComponent<ParticleSystem>()
                    .transform
                    .gameObject
                    .SetActive(true);
                ScoreController.instance.scores++;
                if (!betul.isPlaying)
                {
                    pop.Play();
                    betul.Play();
                }
            }
            else
            {
                soalan.questions[soalan.randomNumber].GetComponent<Image>().color = new Color(
                    0,
                    255,
                    0,
                    255
                );
                if (!salah.isPlaying)
                {
                    salah.Play();
                }
                health.LoseHealth();
            }

            yield return new WaitForSeconds(2f);

            //disable again

            for (int i = 0; i < questionlist.Count; i++)
            {
                questionlist[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(soalan.questions[i].gameObject.GetComponent<Button>());
                soalan.questions[i].gameObject.GetComponent<Button>().enabled = true;
            }
            for (int i = 0; i < soalan.questions.Count; i++)
            {
                soalan.questions[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }

            startTimer = false;
            //clear list of 3 answers
            soalan.questions.Clear();

            //destroy the gameobject
            Destroy(currentTransform);
            level.levels++;
            //start again
            GameObject.Find("btnMula").GetComponent<Button>().interactable = true;
            //StartRandom();
            questionlist.RemoveAt(t);
        }
        else
        { //if time running out
            health.LoseHealth();
            //remove in list
            int t = soalan.questions[soalan.randomNumber].transform.GetSiblingIndex();
            //disable the buttons
            for (int i = 0; i < 3; i++)
            {
                //Debug.Log(soalan.questions[i].gameObject.GetComponent<Button>());
                soalan.questions[i].gameObject.GetComponent<Button>().enabled = false;
            }
            if (!salah.isPlaying)
            {
                salah.Play();
            }

            //change 3 image coilor to red
            for (int i = 0; i < soalan.questions.Count; i++)
            {
                soalan.questions[i].GetComponent<Image>().color = new Color(255, 0, 0, 255);
            }
            soalan.questions[soalan.randomNumber].GetComponent<Image>().color = new Color(
                0,
                255,
                0,
                255
            );
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < questionlist.Count; i++)
            {
                questionlist[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(soalan.questions[i].gameObject.GetComponent<Button>());
                soalan.questions[i].gameObject.GetComponent<Button>().enabled = true;
            }
            for (int i = 0; i < soalan.questions.Count; i++)
            {
                soalan.questions[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }

            //destroy the gameobject
            if (soalan.questions[soalan.randomNumber].name.Equals(soalan.realAnswer))
            {
                Destroy(soalan.questions[soalan.randomNumber].gameObject);
            }

            //clear list of 3 answers
            soalan.questions.Clear();
            startTimer = false;
            level.levels++;
            //start again
            GameObject.Find("btnMula").GetComponent<Button>().interactable = true;
            //StartRandom();
            questionlist.RemoveAt(t);
        }
    }

    public void ClickAnswer()
    {
        StartCoroutine(ChooseAnswer());
    }

    public void StartRandom()
    {
        StartCoroutine(StartingRandom());
    }

    IEnumerator StartingRandom()
    {
        GameObject.Find("btnMula").GetComponent<Button>().interactable = false;
        timeRemaining = 10f;
        timerSlider.value = timeRemaining;
        int[] randomNumCollected = new int[3];
        numbers[0] = Random.Range(0, questionlist.Count);

        numbers[1] = Random.Range(0, questionlist.Count);

        numbers[2] = Random.Range(0, questionlist.Count);

        tryagain:
        if (
            numbers[0] != numbers[1]
            && numbers[0] != numbers[2]
            && numbers[1] != numbers[0]
            && numbers[1] != numbers[2]
        )
        {
            randomNumCollected[0] = numbers[0];
            randomNumCollected[1] = numbers[1];
            randomNumCollected[2] = numbers[2];
            if (level.levels == 1 || level.levels == 3 || level.levels == 6)
            {
                levelDisplay.gameObject.SetActive(true);
                levelDisplay.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "Level " + level.levels;
                levelDisplay.GetComponent<Animator>().Play("loadinglevel");

                yield return new WaitForSeconds(5f);
                // jawapanAnim.Play("showupballoon");
            }

            jawapanAnim.Play("showupballoon");

            for (int i = 0; i < 3; i++)
            {
                //Debug.Log(questionlist[randomNumCollected[i]].name);
                //display
                questionlist[randomNumCollected[i]].gameObject.SetActive(true);
                //interactable
                questionlist[randomNumCollected[i]].GetComponent<Button>().enabled = true;
                questionlist[randomNumCollected[i]].GetComponent<Button>().interactable = true;
            }
            isgamestart = true;

            if (!GameObject.Find("cari").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("cari").GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(2f);
            }

            startTimer = true;
        }
        else
        {
            Debug.Log("not showing");
            while (numbers[0] == numbers[1] || numbers[0] == numbers[2])
            {
                numbers[0] = Random.Range(0, questionlist.Count);
                numbers[1] = Random.Range(0, questionlist.Count);
            }
            while (numbers[1] == numbers[2])
            {
                numbers[2] = Random.Range(0, questionlist.Count);
            }

            goto tryagain;
        }
    }

    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        isHealthEmpty = !health.health.Any();
        if (!isHealthEmpty)
        {
            if (startTimer)
            {
                timerSlider.value = timeRemaining;
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    startTimer = false;
                    ClickAnswer();
                }
            }
        }
        else
        {
            failedDisplay.gameObject.SetActive(true);
            Debug.Log("running out of health");
        }
    }
}

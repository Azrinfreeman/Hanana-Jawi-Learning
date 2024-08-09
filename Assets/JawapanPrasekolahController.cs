using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class JawapanPrasekolahController : MonoBehaviour
{
    [Header("Nama Scene")]
    public string sceneName;
    public bool isGamelvl5;
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
    public Transform winDisplay;

    public Transform btnSound;

    [Header("Audios")]
    public AudioSource claps;
    public AudioSource betul;
    public AudioSource salah;
    public AudioSource pop;

    [Header("Animation")]
    public Animator jawapanAnim;

    [Header("TimerSlider")]
    public Slider timerSlider;

    [Header("Random number 1 - 3 ")] // random number utk pilih 3 soalan dari semua soalan setiap round
    public int[] numbers = new int[3];
    public int randomNumber;

    [Header("Steps")] //utk kira markah
    public int steps = 1;

    //public int stepsSoalan = 1;

    //public int stepsSoalanBetul = 1;

    public int markah = 10;

    [Header("bilangan soalan asal")]
    public int bil;

    // Start is called before the first frame update
    void Initialized()
    {
        sceneName = SceneManager.GetActiveScene().name;
        bil = transform.childCount;
        steps = 1;
        //stepsSoalan = 1;
        //stepsSoalanBetul = 1;

        btnSound = GameObject.Find("btnSound").GetComponent<Transform>();
        jawapanAnim = GetComponent<Animator>();
        failedDisplay = GameObject.Find("FailedDIsplay").GetComponent<Transform>();
        failedDisplay.gameObject.SetActive(false);
        winDisplay = GameObject.Find("WinDisplay").GetComponent<Transform>();
        winDisplay.gameObject.SetActive(false);
        levelDisplay = GameObject.Find("LevelDisplay").GetComponent<Transform>();
        level = GameObject.Find("Canvas").GetComponent<ScoreController>();
        //        health = GameObject.Find("health").GetComponent<HealthController>();
        soalan = GameObject.Find("soalan").GetComponent<QuestionPrasekolahController>();
        betul = GameObject.Find("yay").GetComponent<AudioSource>();
        salah = GameObject.Find("salah").GetComponent<AudioSource>();
        pop = GameObject.Find("pop").GetComponent<AudioSource>();
        claps = GameObject.Find("claps").GetComponent<AudioSource>();
        timerSlider = GameObject.Find("Slider").GetComponent<Slider>();
        levelDisplay.gameObject.SetActive(false);
        btnSound.gameObject.SetActive(false);

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
        //disable the sound button
        GameObject.Find("btnSound").GetComponent<Button>().interactable = false;
        //iftheres time
        //if (timeRemaining > 0)
        //{
        //remove in list
        int t = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();

        Debug.Log("T: " + t);
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
            soalan.questions[i].GetComponent<Image>().color = new Color32(255, 115, 115, 255);
        }

        //check if correct
        if (currentTransform.transform.name.ToString().Equals(soalan.realAnswer))
        {
            //green color
            soalan.questions[randomNumber].GetComponent<Image>().color = new Color32(
                123,
                248,
                123,
                255
            );
            yield return new WaitForSeconds(1f);
            soalan.questions[randomNumber].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            //particle system active
            currentTransform
                .transform.GetChild(1)
                .GetComponent<ParticleSystem>()
                .transform.gameObject.SetActive(true);
            ScoreController.instance.scores++;
            if (!betul.isPlaying)
            {
                pop.Play();
                betul.Play();
                claps.Play();
                //BUTTERFLY VISIBLE
                if (isGamelvl5)
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(5)
                        .transform.gameObject.SetActive(true);

                    for (
                        int i = 0;
                        i < soalan.questions[randomNumber].transform.GetChild(5).childCount;
                        i++
                    )
                    {
                        soalan
                            .questions[randomNumber]
                            .transform.GetChild(5)
                            .GetChild(i)
                            .GetComponent<PatrolController>()
                            .speed = 0f;
                    }
                }
                else
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .transform.gameObject.SetActive(true);

                    for (
                        int i = 0;
                        i < soalan.questions[randomNumber].transform.GetChild(3).childCount;
                        i++
                    )
                    {
                        soalan
                            .questions[randomNumber]
                            .transform.GetChild(3)
                            .GetChild(i)
                            .GetComponent<PatrolController>()
                            .speed = 0f;
                    }
                }
            }

            yield return new WaitForSeconds(1.0f);

            if (isGamelvl5)
            {
                if (timeRemaining > 7 && timeRemaining < 10)
                {
                    markah = 10;
                }
                else if (timeRemaining > 3 && timeRemaining < 7)
                {
                    markah = markah - (10 - (int)timeRemaining);
                }
                else if (timeRemaining < 3)
                {
                    markah = 2;
                }
                //mula animation tambah coin/markah
                MarkahTerkumpulController.instance.startAdd(markah);
                yield return new WaitForSeconds(1.2f);
                for (
                    int i = 0;
                    i < soalan.questions[randomNumber].transform.GetChild(5).childCount;
                    i++
                )
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(5)
                        .GetChild(i)
                        .GetComponent<PatrolController>()
                        .speed = 4f;
                }
            }
            else
            {
                if (timeRemaining > 7 && timeRemaining < 10)
                {
                    markah = 10;
                }
                else if (timeRemaining > 3 && timeRemaining < 7)
                {
                    markah = markah - (10 - (int)timeRemaining);
                }
                else if (timeRemaining < 3)
                {
                    markah = 2;
                }
                //mula animation tambah coin/markah
                MarkahTerkumpulController.instance.startAdd(markah);
                yield return new WaitForSeconds(1.2f);
                //butterfly moving
                for (
                    int i = 0;
                    i < soalan.questions[randomNumber].transform.GetChild(3).childCount;
                    i++
                )
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .GetChild(i)
                        .GetComponent<PatrolController>()
                        .speed = 4f;
                }
            }
            //add to steps if correct
            steps++;
            //stepsSoalanBetul++;
            yield return new WaitForSeconds(1f);

            //remove the gameobect and list
            questionlist.RemoveAt(t);
            yield return new WaitForSeconds(1f);

            //EventSystem.current.currentSelectedGameObject.transform.SetParent(null);
            //EventSystem.current.currentSelectedGameObject.transform.gameObject.SetActive(false);
            currentTransform.transform.SetParent(null);
            currentTransform.SetActive(false);
            Debug.Log("questlion list removed");
        }
        //check if WRONG
        else
        {
            soalan.questions[randomNumber].GetComponent<Image>().color = new Color32(
                123,
                248,
                123,
                255
            );
            if (!salah.isPlaying)
            {
                salah.Play();
            }
            //health.LoseHealth();
            yield return new WaitForSeconds(3f);
        }

        //disable again

        for (int i = 0; i < questionlist.Count; i++)
        {
            questionlist[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            //            Debug.Log(soalan.questions[i].gameObject.GetComponent<Button>());
            soalan.questions[i].gameObject.GetComponent<Button>().enabled = true;
        }
        for (int i = 0; i < soalan.questions.Count; i++)
        {
            soalan.questions[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        startTimer = false;
        //clear list of 3 answers
        soalan.questions.Clear();

        //destroy the gameobject
        //Destroy(currentTransform);
        //level.levels++;
        //start again
        GameObject.Find("btnMula").GetComponent<Button>().interactable = true;
        btnSound.transform.gameObject.SetActive(false);
        //StartRandom();



        if (questionlist.Count < 3)
        {
            GameObject.Find("btnMula").GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(1.5f);
            if (steps > PlayerPrefs.GetInt(sceneName + "HighScore") && steps > 1)
            {
                PlayerPrefs.SetInt(sceneName + "HighScore", steps);
            }
            //show win menu
            //winDisplay.gameObject.SetActive(true);

            //restart scene seamelessly
            PlayerPrefs.SetInt("Dialog_" + SceneManager.GetActiveScene().name, 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // stepsSoalan++;
        //}
        /*
        else
        { //if time running out
            StartCoroutine(timeRunningOut());
        }
        */
    }

    IEnumerator selectAnswer()
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
            soalan.questions[i].GetComponent<Image>().color = new Color32(255, 115, 115, 255);
        }

        //check if correct
        if (currentTransform.transform.name.ToString().Equals(soalan.realAnswer))
        {
            //green color
            soalan.questions[randomNumber].GetComponent<Image>().color = new Color32(
                123,
                248,
                123,
                255
            );
            yield return new WaitForSeconds(1f);
            soalan.questions[randomNumber].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            //particle system active
            currentTransform
                .transform.GetChild(1)
                .GetComponent<ParticleSystem>()
                .transform.gameObject.SetActive(true);
            ScoreController.instance.scores++;
            if (!betul.isPlaying)
            {
                pop.Play();
                betul.Play();
                //BUTTERFLY VISIBLE
                if (isGamelvl5)
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(5)
                        .transform.gameObject.SetActive(true);

                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(5)
                        .GetChild(0)
                        .GetComponent<PatrolController>()
                        .speed = 0f;

                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(5)
                        .GetChild(1)
                        .GetComponent<PatrolController>()
                        .speed = 0f;
                }
                else
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .transform.gameObject.SetActive(true);

                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .GetChild(0)
                        .GetComponent<PatrolController>()
                        .speed = 0f;

                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .GetChild(1)
                        .GetComponent<PatrolController>()
                        .speed = 0f;

                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .GetChild(2)
                        .GetComponent<PatrolController>()
                        .speed = 0f;

                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .GetChild(3)
                        .GetComponent<PatrolController>()
                        .speed = 0f;
                }
            }

            yield return new WaitForSeconds(1.8f);

            if (isGamelvl5)
            {
                soalan
                    .questions[randomNumber]
                    .transform.GetChild(5)
                    .GetChild(0)
                    .GetComponent<PatrolController>()
                    .speed = 4f;

                soalan
                    .questions[randomNumber]
                    .transform.GetChild(5)
                    .GetChild(1)
                    .GetComponent<PatrolController>()
                    .speed = 4f;
            }
            else
            {
                if (timeRemaining > 7 && timeRemaining < 10)
                {
                    markah = 10;
                }
                else if (timeRemaining > 3 && timeRemaining < 7)
                {
                    markah = markah - (10 - (int)timeRemaining);
                }
                else if (timeRemaining < 3)
                {
                    markah = 2;
                }
                //mula animation tambah coin/markah
                MarkahTerkumpulController.instance.startAdd(markah);
                yield return new WaitForSeconds(1.2f);
                soalan
                    .questions[randomNumber]
                    .transform.GetChild(3)
                    .GetChild(0)
                    .GetComponent<PatrolController>()
                    .speed = 4f;

                soalan
                    .questions[randomNumber]
                    .transform.GetChild(3)
                    .GetChild(1)
                    .GetComponent<PatrolController>()
                    .speed = 4f;
            }
            //add to steps if correct
            steps++;
            //stepsSoalanBetul++;
            yield return new WaitForSeconds(4f);
        }
        else
        {
            soalan.questions[randomNumber].GetComponent<Image>().color = new Color32(
                123,
                248,
                123,
                255
            );
            if (!salah.isPlaying)
            {
                salah.Play();
            }
            //health.LoseHealth();
            yield return new WaitForSeconds(3f);
        }

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
            soalan.questions[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        startTimer = false;
        //clear list of 3 answers
        soalan.questions.Clear();

        //destroy the gameobject
        Destroy(currentTransform);
        //level.levels++;
        //start again
        GameObject.Find("btnMula").GetComponent<Button>().interactable = true;
        btnSound.transform.gameObject.SetActive(false);
        //StartRandom();
        questionlist.RemoveAt(t);

        if (questionlist.Count < 3)
        {
            GameObject.Find("btnMula").GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(2f);
            if (steps > PlayerPrefs.GetInt(sceneName + "HighScore") && steps > 1)
            {
                PlayerPrefs.SetInt(sceneName + "HighScore", steps);
            }
            winDisplay.gameObject.SetActive(true);
        }
        // stepsSoalan++;
    }

    IEnumerator timeRunningOut()
    {
        //health.LoseHealth();
        //remove in list
        int t = soalan.questions[randomNumber].transform.GetSiblingIndex();
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
            soalan.questions[i].GetComponent<Image>().color = new Color32(255, 115, 155, 255);
        }
        //green
        soalan.questions[randomNumber].GetComponent<Image>().color = new Color32(
            123,
            248,
            123,
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
        { //white normal color
            soalan.questions[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        //destroy the gameobject
        if (soalan.questions[randomNumber].name.Equals(soalan.realAnswer))
        {
            Destroy(soalan.questions[randomNumber].gameObject);
        }

        //clear list of 3 answers
        soalan.questions.Clear();
        startTimer = false;
        // level.levels++;
        //start again
        GameObject.Find("btnMula").GetComponent<Button>().interactable = true;
        btnSound.transform.gameObject.SetActive(false);
        //StartRandom();
        questionlist.RemoveAt(t);
        if (questionlist.Count < 3)
        {
            GameObject.Find("btnMula").GetComponent<Button>().interactable = false;
            if (steps > PlayerPrefs.GetInt(sceneName + "HighScore") && steps > 1)
            {
                PlayerPrefs.SetInt(sceneName + "HighScore", steps);
            }
            yield return new WaitForSeconds(2f);

            winDisplay.gameObject.SetActive(true);
        }
        // stepsSoalan++;
    }

    public void ClickAnswer()
    {
        StartCoroutine(ChooseAnswer());
    }

    public void StartRandom()
    {
        StartCoroutine(StartingRandom());
    }

    public void PlaySound()
    {
        StartCoroutine(SoundPlay());
    }

    IEnumerator SoundPlay()
    {
        if (!isGamelvl5)
        {
            if (
                !soalan
                    .questions[randomNumber]
                    .transform.GetChild(2)
                    .GetComponent<AudioSource>()
                    .isPlaying
            )
            {
                soalan
                    .questions[randomNumber]
                    .transform.GetChild(2)
                    .GetComponent<AudioSource>()
                    .Play();
            }
        }
        else
        {
            if (
                !soalan
                    .questions[randomNumber]
                    .transform.GetChild(3)
                    .GetComponent<AudioSource>()
                    .isPlaying
            )
            {
                soalan
                    .questions[randomNumber]
                    .transform.GetChild(3)
                    .GetComponent<AudioSource>()
                    .Play();
            }
            yield return new WaitForSeconds(1.2f);
            if (
                !soalan
                    .questions[randomNumber]
                    .transform.GetChild(4)
                    .GetComponent<AudioSource>()
                    .isPlaying
            )
            {
                soalan
                    .questions[randomNumber]
                    .transform.GetChild(4)
                    .GetComponent<AudioSource>()
                    .Play();
            }
            yield return new WaitForSeconds(1.2f);
            if (
                !soalan
                    .questions[randomNumber]
                    .transform.GetChild(2)
                    .GetComponent<AudioSource>()
                    .isPlaying
            )
            {
                soalan
                    .questions[randomNumber]
                    .transform.GetChild(2)
                    .GetComponent<AudioSource>()
                    .Play();
            }
        }
    }

    IEnumerator StartingRandom()
    {
        GameObject.Find("btnMula").GetComponent<Button>().interactable = false;
        GameObject
            .Find("btnMula")
            .transform.parent.GetChild(1)
            .GetComponent<Button>()
            .interactable = true;

        timeRemaining = 10f;
        timerSlider.value = timeRemaining;
        int[] randomNumCollected = new int[3];
        numbers[0] = Random.Range(0, questionlist.Count);

        numbers[1] = Random.Range(0, questionlist.Count);

        numbers[2] = Random.Range(0, questionlist.Count);
        randomNumber = Random.Range(0, 3);
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
                //levelDisplay.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                //   "Level " + level.levels;
                levelDisplay.GetComponent<Animator>().Play("loadinglevel");
                level.levels++;
                yield return new WaitForSeconds(2.5f);
                // jawapanAnim.Play("showupballoon");
            }

            jawapanAnim.Play("showupballoon");

            for (int i = 0; i < 3; i++)
            {
                //Debug.Log(questionlist[randomNumCollected[i]].name);
                //display
                questionlist[randomNumCollected[i]].gameObject.SetActive(true);
                questionlist[randomNumCollected[i]].GetComponent<Button>().interactable = true;
            }
            isgamestart = true;
            // if game level 5 or not
            if (!isGamelvl5)
            {
                /*
                if (!GameObject.Find("cari").GetComponent<AudioSource>().isPlaying)
                {
                    GameObject.Find("cari").GetComponent<AudioSource>().Play();
                    while (GameObject.Find("cari").GetComponent<AudioSource>().isPlaying)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.8f);
                }
                */
                yield return new WaitForSeconds(0.8f);
                if (
                    !soalan
                        .questions[randomNumber]
                        .transform.GetChild(2)
                        .GetComponent<AudioSource>()
                        .isPlaying
                )
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(2)
                        .GetComponent<AudioSource>()
                        .Play();

                    while (
                        soalan
                            .questions[randomNumber]
                            .transform.GetChild(2)
                            .GetComponent<AudioSource>()
                            .isPlaying
                    )
                    {
                        yield return null;
                    }
                }

                btnSound.transform.gameObject.SetActive(true);
                for (int i = 0; i < 3; i++)
                {
                    //interactable
                    questionlist[randomNumCollected[i]].GetComponent<Button>().enabled = true;
                }
                startTimer = true;
            }
            else
            {
                /*
                if (!GameObject.Find("cari").GetComponent<AudioSource>().isPlaying)
                {
                    GameObject.Find("cari").GetComponent<AudioSource>().Play();
                    while (GameObject.Find("cari").GetComponent<AudioSource>().isPlaying)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.8f);
                }
                */
                yield return new WaitForSeconds(0.8f);
                /*if (
                    !soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .GetComponent<AudioSource>()
                        .isPlaying
                )
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(3)
                        .GetComponent<AudioSource>()
                        .Play();
                }
                yield return new WaitForSeconds(1.2f);
                */
                /*if (
                    !soalan
                        .questions[randomNumber]
                        .transform.GetChild(4)
                        .GetComponent<AudioSource>()
                        .isPlaying
                )
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(4)
                        .GetComponent<AudioSource>()
                        .Play();
                }
                yield return new WaitForSeconds(1.2f);
                */
                if (
                    !soalan
                        .questions[randomNumber]
                        .transform.GetChild(2)
                        .GetComponent<AudioSource>()
                        .isPlaying
                )
                {
                    soalan
                        .questions[randomNumber]
                        .transform.GetChild(2)
                        .GetComponent<AudioSource>()
                        .Play();
                }
                yield return new WaitForSeconds(2.3f);
                btnSound.transform.gameObject.SetActive(true);
                for (int i = 0; i < 3; i++)
                {
                    //interactable
                    questionlist[randomNumCollected[i]].GetComponent<Button>().enabled = true;
                }
                startTimer = true;
            }
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
        /*
        isHealthEmpty = !health.health.Any();
        if (!isHealthEmpty)
        {
           

        }
        else
        {
            if (steps > PlayerPrefs.GetInt(sceneName + "HighScore") && steps > 1)
            {
                PlayerPrefs.SetInt(sceneName + "HighScore", steps);
            }

            failedDisplay.gameObject.SetActive(true);

            // Debug.Log("running out of health");
        }
*/
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
                //ClickAnswer();
            }
        }

        /*
        if (stepsSoalanBetul > 5)
        {
            failedDisplay.gameObject.SetActive(true);
        }
        */
    }

    public void resetStep()
    {
        //stepsSoalan = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneName;
    public bool isSplashScreen;

    public float duration = 4f;

    public AudioSource startAudio;

    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isSplashScreen)
        {
            StartGame();
        }
        else
        {
            //compare scene
            StartCoroutine(StartCari());
        }
    }

    IEnumerator StartCari()
    {
        if (
            sceneName.Equals("P1L1")
            || sceneName.Equals("P1L2")
            || sceneName.Equals("P1L3")
            || sceneName.Equals("P1L4")
            || sceneName.Equals("P1L5")
            || sceneName.Equals("P1L6")
        )
        {
            if (!GameObject.Find("cari").GetComponent<AudioSource>().isPlaying)
            {
                yield return new WaitForSeconds(1f);
                GameObject.Find("cari").GetComponent<AudioSource>().Play();
            }
        }
    }

    // Update is called once per frame
    void Update() { }

    IEnumerator changing(string sceneName, float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        StartCoroutine(Reload());
    }

    public void ChangeSceneNoDelay(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(changing(sceneName, duration));
    }

    IEnumerator startgame()
    {
        yield return new WaitForSeconds(2.2f);
        startAudio.gameObject.SetActive(true);
        while (startAudio.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene("FrontMenu");
    }

    public void StartGame()
    {
        StartCoroutine(startgame());
    }

    public void LoadAllAnimalsInZoo()
    {
        PlayerPrefs.SetInt("P1L1_0", 1);
        PlayerPrefs.SetInt("P1L1_1", 1);
        PlayerPrefs.SetInt("P1L1_2", 1);
        PlayerPrefs.SetInt("P1L1_3", 1);
        PlayerPrefs.SetInt("P1L1_4", 1);

        PlayerPrefs.SetInt("P1L2_0", 1);
        PlayerPrefs.SetInt("P1L2_1", 1);
        PlayerPrefs.SetInt("P1L2_2", 1);
        PlayerPrefs.SetInt("P1L2_3", 1);
        PlayerPrefs.SetInt("P1L2_4", 1);

        PlayerPrefs.SetInt("P1L3_0", 1);
        PlayerPrefs.SetInt("P1L3_1", 1);
        PlayerPrefs.SetInt("P1L3_2", 1);
        PlayerPrefs.SetInt("P1L3_3", 1);
        PlayerPrefs.SetInt("P1L3_4", 1);

        PlayerPrefs.SetInt("P1L4_0", 1);
        PlayerPrefs.SetInt("P1L4_1", 1);
        PlayerPrefs.SetInt("P1L4_2", 1);
        PlayerPrefs.SetInt("P1L4_3", 1);
        PlayerPrefs.SetInt("P1L4_4", 1);
        PlayerPrefs.SetInt("P1L4_5", 1);
        PlayerPrefs.SetInt("P1L4_6", 1);
        PlayerPrefs.SetInt("P1L4_7", 1);
        PlayerPrefs.SetInt("P1L4_8", 1);
        PlayerPrefs.SetInt("P1L4_9", 1);

        PlayerPrefs.SetInt("P1L5_0", 1);
        PlayerPrefs.SetInt("P1L5_1", 1);
        PlayerPrefs.SetInt("P1L5_2", 1);
        PlayerPrefs.SetInt("P1L5_3", 1);
        PlayerPrefs.SetInt("P1L5_4", 1);
        PlayerPrefs.SetInt("P1L5_5", 1);
        PlayerPrefs.SetInt("P1L5_6", 1);
        PlayerPrefs.SetInt("P1L5_7", 1);
        PlayerPrefs.SetInt("P1L5_8", 1);
        PlayerPrefs.SetInt("P1L5_9", 1);
        PlayerPrefs.SetInt("P1L5_10", 1);
        PlayerPrefs.SetInt("P1L5_11", 1);
        PlayerPrefs.SetInt("P1L5_12", 1);
        PlayerPrefs.SetInt("P1L5_13", 1);
        PlayerPrefs.SetInt("P1L5_14", 1);

        PlayerPrefs.SetInt("P1L6_0", 1);
        PlayerPrefs.SetInt("P1L6_1", 1);
        PlayerPrefs.SetInt("P1L6_2", 1);
        PlayerPrefs.SetInt("P1L6_3", 1);
        PlayerPrefs.SetInt("P1L6_4", 1);
        PlayerPrefs.SetInt("P1L6_5", 1);
        PlayerPrefs.SetInt("P1L6_6", 1);
        PlayerPrefs.SetInt("P1L6_7", 1);
        PlayerPrefs.SetInt("P1L6_8", 1);
        PlayerPrefs.SetInt("P1L6_9", 1);
        PlayerPrefs.SetInt("P1L6_10", 1);
        PlayerPrefs.SetInt("P1L6_11", 1);
        PlayerPrefs.SetInt("P1L6_12", 1);
        PlayerPrefs.SetInt("P1L6_13", 1);
        PlayerPrefs.SetInt("P1L6_14", 1);
        PlayerPrefs.SetInt("P1L6_15", 1);
        PlayerPrefs.SetInt("P1L6_16", 1);
        PlayerPrefs.SetInt("P1L6_17", 1);
        PlayerPrefs.SetInt("P1L6_18", 1);
        PlayerPrefs.SetInt("P1L6_19", 1);
        PlayerPrefs.SetInt("P1L6_20", 1);
        PlayerPrefs.SetInt("P1L6_21", 1);
        PlayerPrefs.SetInt("P1L6_22", 1);
        PlayerPrefs.SetInt("P1L6_23", 1);
        PlayerPrefs.SetInt("P1L6_24", 1);
        PlayerPrefs.SetInt("P1L6_25", 1);
        PlayerPrefs.SetInt("P1L6_26", 1);
        PlayerPrefs.SetInt("P1L6_27", 1);
        PlayerPrefs.SetInt("P1L6_28", 1);
        PlayerPrefs.SetInt("P1L6_29", 1);
        PlayerPrefs.SetInt("P1L6_30", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UnloadAllAnimalsInZoo()
    {
        PlayerPrefs.SetInt("P1L1_0", 0);
        PlayerPrefs.SetInt("P1L1_1", 0);
        PlayerPrefs.SetInt("P1L1_2", 0);
        PlayerPrefs.SetInt("P1L1_3", 0);
        PlayerPrefs.SetInt("P1L1_4", 0);

        PlayerPrefs.SetInt("P1L2_0", 0);
        PlayerPrefs.SetInt("P1L2_1", 0);
        PlayerPrefs.SetInt("P1L2_2", 0);
        PlayerPrefs.SetInt("P1L2_3", 0);
        PlayerPrefs.SetInt("P1L2_4", 0);

        PlayerPrefs.SetInt("P1L3_0", 0);
        PlayerPrefs.SetInt("P1L3_1", 0);
        PlayerPrefs.SetInt("P1L3_2", 0);
        PlayerPrefs.SetInt("P1L3_3", 0);
        PlayerPrefs.SetInt("P1L3_4", 0);

        PlayerPrefs.SetInt("P1L4_0", 0);
        PlayerPrefs.SetInt("P1L4_1", 0);
        PlayerPrefs.SetInt("P1L4_2", 0);
        PlayerPrefs.SetInt("P1L4_3", 0);
        PlayerPrefs.SetInt("P1L4_4", 0);
        PlayerPrefs.SetInt("P1L4_5", 0);
        PlayerPrefs.SetInt("P1L4_6", 0);
        PlayerPrefs.SetInt("P1L4_7", 0);
        PlayerPrefs.SetInt("P1L4_8", 0);
        PlayerPrefs.SetInt("P1L4_9", 0);

        PlayerPrefs.SetInt("P1L5_0", 0);
        PlayerPrefs.SetInt("P1L5_1", 0);
        PlayerPrefs.SetInt("P1L5_2", 0);
        PlayerPrefs.SetInt("P1L5_3", 0);
        PlayerPrefs.SetInt("P1L5_4", 0);
        PlayerPrefs.SetInt("P1L5_5", 0);
        PlayerPrefs.SetInt("P1L5_6", 0);
        PlayerPrefs.SetInt("P1L5_7", 0);
        PlayerPrefs.SetInt("P1L5_8", 0);
        PlayerPrefs.SetInt("P1L5_9", 0);
        PlayerPrefs.SetInt("P1L5_10", 0);
        PlayerPrefs.SetInt("P1L5_11", 0);
        PlayerPrefs.SetInt("P1L5_12", 0);
        PlayerPrefs.SetInt("P1L5_13", 0);
        PlayerPrefs.SetInt("P1L5_14", 0);

        PlayerPrefs.SetInt("P1L6_0", 0);
        PlayerPrefs.SetInt("P1L6_1", 0);
        PlayerPrefs.SetInt("P1L6_2", 0);
        PlayerPrefs.SetInt("P1L6_3", 0);
        PlayerPrefs.SetInt("P1L6_4", 0);
        PlayerPrefs.SetInt("P1L6_5", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

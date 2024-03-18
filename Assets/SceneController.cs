using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public bool isSplashScreen;

    public float duration = 4f;
    // Start is called before the first frame update
    void Start() {
        if(isSplashScreen){
            StartGame();
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

    public void ExitApp(){
        Application.Quit();
    }

    public void ReloadScene()
    {
        StartCoroutine(Reload());
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(changing(sceneName, duration));
    }

    IEnumerator startgame()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("FrontMenu");
    }

    public void StartGame()
    {
        StartCoroutine(startgame());
    }
}

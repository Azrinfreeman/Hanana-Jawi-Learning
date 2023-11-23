using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    IEnumerator changing(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(changing(sceneName));
    }
}

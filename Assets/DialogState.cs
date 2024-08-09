using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aek());
    }

    void Awake() { }

    IEnumerator aek()
    {
        if (PlayerPrefs.GetInt("Dialog_" + SceneManager.GetActiveScene().name) == 1)
        {
            GetComponent<Animator>().Play("DialogEnd");
            yield return new WaitForSeconds(1.2f);
            PlayerPrefs.SetInt("Dialog_" + SceneManager.GetActiveScene().name, 0);
        }
    }

    // Update is called once per frame
    void Update() { }
}

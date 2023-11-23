using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class atmostController : MonoBehaviour
{
    public Transform[] stars;
    public Transform[] atmost;

    [Header("Only for main menu")]
    public TextMeshProUGUI titleText;

    // Start is called before the first frame update

    void Start()
    {
        stars = new Transform[2];
        atmost = new Transform[2];
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name.Equals("FrontMenu"))
        {
            titleText = GameObject.Find("title").GetComponent<TextMeshProUGUI>();
        }

        for (int i = 0; i < 2; i++)
        {
            stars[i] = GameObject.Find("sunormoon").transform.GetChild(i).GetComponent<Transform>();
            atmost[i] = GameObject.Find("atmost").transform.GetChild(i).GetComponent<Transform>();
        }

        if (System.DateTime.Now.Hour >= 7 && System.DateTime.Now.Hour <= 19)
        {
            // It's on or after 7 AM!
            stars[0].gameObject.SetActive(true);
            stars[1].gameObject.SetActive(false);
            atmost[0].gameObject.SetActive(true);
            atmost[1].gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name.Equals("FrontMenu"))
            {
                titleText.color = Color.black;
            }
            Debug.Log(System.DateTime.Now.ToString("h:mm:ss tt"));
        }
        else
        { // It's on or after 7 PM!
            stars[0].gameObject.SetActive(false);
            stars[1].gameObject.SetActive(true);
            atmost[1].gameObject.SetActive(true);
            atmost[0].gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name.Equals("FrontMenu"))
            {
                titleText.color = Color.white;
            }
            Debug.Log(System.DateTime.Now.ToString("h:mm:ss tt"));
        }
    }

    // Update is called once per frame
    void Update() { }
}

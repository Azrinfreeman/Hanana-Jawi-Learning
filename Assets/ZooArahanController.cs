using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZooArahanController : MonoBehaviour
{
    public static ZooArahanController instance;
    public AudioSource audioSource;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            audioSource = gameObject.GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(instance);
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "FrontMenu")
        {
            Destroy(gameObject);
        }
    }
}

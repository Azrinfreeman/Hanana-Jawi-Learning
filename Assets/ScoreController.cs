using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    void Awake()
    {
        instance = this;
    }

    public int levels = 0;
    public int scores = 0;

    // Start is called before the first frame update
    void Start()
    {
        levels = 1;
    }

    // Update is called once per frame
    void Update() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void ResetPreferences()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CURRENT_TROPHY_PLAYER_" + PlayerPrefs.GetInt("CurrentPlayerNo_"), 0);
        PlayerPrefs.SetInt("MarkahTerkumpul_" + PlayerPrefs.GetInt("CurrentPlayerNo_"), 0);
    }
}

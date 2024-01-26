using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class loadSteps : MonoBehaviour
{
    public TextMeshProUGUI text;
    public JawapanPrasekolahController jawapan;

    public bool isHighScore;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (isHighScore)
        {
            text.text = (PlayerPrefs.GetInt(jawapan.sceneName+"HighScore") * 10).ToString();
        }
        else
        {

            text.text = (jawapan.steps * 10).ToString();
        }
        //Debug.Log("steps: " + jawapan.steps);
    }
}

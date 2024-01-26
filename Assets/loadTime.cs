using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
public class loadTime : MonoBehaviour
{
    public JawapanPrasekolahController jawapan;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text =GetComponent<TextMeshProUGUI>();
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = jawapan.timeRemaining.ToString("00");
    }
}

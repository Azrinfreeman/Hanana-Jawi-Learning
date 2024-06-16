using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class loadSoalan : MonoBehaviour
{
    public TextMeshProUGUI text;
    public JawapanPrasekolahController jawapan;

    // Start is called before the first frame update
    void Start()
    {
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // text.text = jawapan.stepsSoalanBetul.ToString() + "/" + "5";
    }
}

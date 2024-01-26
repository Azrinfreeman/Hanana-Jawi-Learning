using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPrasekolahController : MonoBehaviour
{
    //public Image imageAnswer;
    public JawapanPrasekolahController jawapan;
    public List<Transform> questions;
    public string realAnswer;

    // Start is called before the first frame update
    void Start()
    {
        //imageAnswer = transform.GetChild(0).transform.GetChild(1).transform.GetComponent<Image>();
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jawapan.isgamestart)
        {
            for (int i = 0; i < jawapan.transform.childCount; i++)
            {
                if (jawapan.transform.GetChild(i).gameObject.activeSelf)
                {
                    Debug.Log(jawapan.transform.GetChild(i).gameObject.name);
                    questions.Add(jawapan.transform.GetChild(i));
                }
            }
            //pick the item using random number and display answer to the question
            // randomNumber = Random.Range(0, 3);

            realAnswer = questions[jawapan.randomNumber].name;
            //imageAnswer.sprite = questions[jawapan.randomNumber].GetChild(0).GetComponent<Image>().sprite;
            jawapan.isgamestart = false;
        }
    }
}

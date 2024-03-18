using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MarkahTerkumpulController : MonoBehaviour
{
    public static MarkahTerkumpulController instance;

    void Awake()
    {
        instance = this;
    }
    public int markahTerkumpul;
    public int tambahanMarkah;
    public TextMeshProUGUI textMarkah;
    public TextMeshProUGUI textTambahan;
    // Start is called before the first frame update
    void Start()
    {
        markahTerkumpul = PlayerPrefs.GetInt("MarkahTerkumpul");
        tambahanMarkah = 20;
        textMarkah.text = markahTerkumpul.ToString();
        textTambahan.text = tambahanMarkah.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startAdd(float markah)
    {
        StartCoroutine(startAdding(markah));
    }
    IEnumerator startAdding(float markah)
    {
        /* method 1
        textMarkah.text = markahTerkumpul.ToString();
        textTambahan.text = tambahanMarkah.ToString();
        GetComponent<Animator>().SetTrigger("addTrigger");
        yield return new WaitForSeconds(1f);
        //green
        textMarkah.color = new Color32(127, 255, 115, 255);
        markahTerkumpul += tambahanMarkah;
        textMarkah.text = markahTerkumpul.ToString();
        textTambahan.text = tambahanMarkah.ToString();
        yield return new WaitForSeconds(1f);
        //turn  whirte again
        textMarkah.color = new Color32(255, 255, 255, 255);
        */
        textMarkah.text = markahTerkumpul.ToString();
        textTambahan.text = markah.ToString();
        GetComponent<Animator>().SetTrigger("addTrigger");
        yield return new WaitForSeconds(1.4f);
        if (!transform.GetChild(3).GetComponent<AudioSource>().isPlaying)
        {

            transform.GetChild(3).GetComponent<AudioSource>().Play();

        }//green
        textMarkah.color = new Color32(127, 255, 115, 255);
        for (int i = 1; i <= markah; i++)
        {
            yield return new WaitForSeconds(0.1f);
            markahTerkumpul += 1;
            textMarkah.text = markahTerkumpul.ToString();
        }

        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("MarkahTerkumpul", markahTerkumpul);
        //turn  whirte again
        textMarkah.color = new Color32(255, 255, 255, 255);
    }
}

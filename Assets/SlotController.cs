using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public Transform[] slots = new Transform[3];
    public int corrects;

    [Header("References")]
    JawapanSusunanController jawapan;

    public Button btnSound;


    public bool isComplete;

    public HealthController health;
    // Start is called before the first frame update
    void Start()
    {
        isComplete = false;
        health = GameObject.Find("health").GetComponent<HealthController>();
        btnSound = GameObject.Find("btnSound").GetComponent<Button>();
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanSusunanController>();
        slots = new Transform[3];
        corrects = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = transform.GetChild(i).transform;
        }
    }

    IEnumerator startReading()
    {
        isComplete = true;
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

        //highlight and play 
        yield return new WaitForSeconds(1.2f);
        slots[2].GetChild(1).transform.GetChild(1).GetComponent<AudioSource>().Play();
        slots[2].GetChild(0).GetComponent<Image>().color = new Color(105, 299, 0, 255);
        if (slots[2].GetChild(1).GetComponent<DragController>().Answer == "1")
        {
            corrects++;
        }
        //highlight and play 
        yield return new WaitForSeconds(1.5f);
        slots[1].GetChild(1).transform.GetChild(1).GetComponent<AudioSource>().Play();
        slots[1].GetChild(0).GetComponent<Image>().color = new Color(105, 299, 0, 255);
        slots[2].GetChild(0).GetComponent<Image>().color = new Color(195, 195, 195, 255);

        if (slots[1].GetChild(1).GetComponent<DragController>().Answer == "2")
        {
            corrects++;
        }

        //highlight and play 
        yield return new WaitForSeconds(1.5f);
        slots[0].GetChild(1).transform.GetChild(1).GetComponent<AudioSource>().Play();
        slots[0].GetChild(0).GetComponent<Image>().color = new Color(105, 299, 0, 255);
        slots[1].GetChild(0).GetComponent<Image>().color = new Color(195, 195, 195, 255);

        if (slots[0].GetChild(1).GetComponent<DragController>().Answer == "3")
        {
            corrects++;
        }
        //return highlited to normal color
        yield return new WaitForSeconds(1.9f);
        slots[0].GetChild(0).GetComponent<Image>().color = new Color(195, 195, 195, 255);


        //Check if all correct 
        if (corrects == 3)
        {
            //jawapan.nextDisplayLevel.gameObject.SetActive(true);
            corrects = 0;
            GameObject.Find("yay").GetComponent<AudioSource>().Play();
            //destroy the placed answeres on the box
            for (int i = 0; i < 3; i++)
            {
                Destroy(slots[i].GetChild(1).gameObject);
            }
            ButterflyController.instance.butterfly[0].gameObject.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            Destroy(ButterflyController.instance.butterfly[0].gameObject);
            ButterflyController.instance.butterfly.RemoveAt(0);

            yield return new WaitForSeconds(1.5f);
            jawapan.steps++;
            jawapan.NextLevel();
        }
        else
        {

            health.LoseHealth();
            corrects = 0;
            GameObject.Find("salah").GetComponent<AudioSource>().Play();
            //destroy the placed answeres on the box
            for (int i = 0; i < 3; i++)
            {
                Destroy(slots[i].GetChild(1).gameObject);
            }
            yield return new WaitForSeconds(1.5f);
            jawapan.NextLevel();
        }


        isComplete = false;
        //Debug.Log("end");
    }

    public void startRead()
    {
        StartCoroutine(startReading());
    }

    // Update is called once per frame
    void Update()
    {
        if (slots[0].GetComponent<SlotJawapan>().answerSlot != "" && slots[1].GetComponent<SlotJawapan>().answerSlot != "" && slots[2].GetComponent<SlotJawapan>().answerSlot != "")
        {
            if (!isComplete)
            {

                btnSound.interactable = true;
                btnSound.transform.GetChild(1).gameObject.SetActive(true);

            }
        }
        else
        {
            btnSound.interactable = false;

            btnSound.transform.GetChild(1).gameObject.SetActive(false);

        }
    }
}

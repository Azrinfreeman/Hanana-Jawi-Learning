using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotJawapan : MonoBehaviour, IDropHandler
{
    public string answerSlot;

    public Transform currentObjectOnSlot;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag);
        if (eventData.pointerDrag != null && currentObjectOnSlot == null)
        {
            if (answerSlot == "")
            {
                GameObject.Find("pop").GetComponent<AudioSource>().Play();
                Debug.Log(eventData.pointerDrag);
                eventData.pointerDrag.GetComponent<Transform>().transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = new Vector3(0.1f, 0.1f, 0.1f);

                //transform.GetChild(0).gameObject.SetActive(false);
                answerSlot = eventData.pointerDrag.GetComponent<DragController>().Answer;

                if (eventData.pointerDrag.tag == "dragable (temp)")
                {
                    eventData.pointerDrag.tag = "dragable";
                    currentObjectOnSlot = eventData.pointerDrag.GetComponent<Transform>();


                }
            }
        }else{
            //return to original position
            Debug.Log("reset positionsssss");
            eventData.pointerDrag.transform.localPosition = eventData.pointerDrag.GetComponent<DragController>().originalPosition;
        }


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "dragable")
        {
            other.gameObject.tag = "dragable (temp)";
            Debug.Log("return to sender");
            answerSlot = "";
            currentObjectOnSlot = null;
            //transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Original Info")]
    public Vector3 originalPosition;
    public Transform originalParent;

    [Header("Canvas and Colider")]

    [SerializeField] private Canvas canvas;
    [SerializeField] private BoxCollider2D colider;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [Header("answer")]
    public string Answer;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        colider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //  Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        GameObject.Find("cartoon").GetComponent<AudioSource>().Play();
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //   Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GetComponent<BoxCollider2D>().enabled = true;



    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

}

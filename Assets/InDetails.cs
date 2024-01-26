using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InDetails : MonoBehaviour
{
    public static InDetails instance;

    void Awake()
    {
        instance = this;
    }
    public List<TextMeshProUGUI> textList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            textList.Add(transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}

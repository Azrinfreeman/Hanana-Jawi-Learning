using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class progressController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (RewardController.instance.isAtEnd)
        {
            transform.GetComponent<TextMeshProUGUI>().text = "COMPLETED";
            transform.GetComponent<TextMeshProUGUI>().color = Color.green;

        }
        else
        {
            transform.GetComponent<TextMeshProUGUI>().text = "ONGOING";
            transform.GetComponent<TextMeshProUGUI>().color = Color.yellow;

        }
    }
}

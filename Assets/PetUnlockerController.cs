using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetUnlockerController : MonoBehaviour
{
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.name;

        if (PlayerPrefs.GetInt(text) == 0)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            transform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BtnAnimate());
    }

    private void BtnAnimate()
    {
        GetComponent<Animator>().SetTrigger("click");
    }

    // Update is called once per frame
    void Update() { }
}

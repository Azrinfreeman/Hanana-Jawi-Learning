using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class rewardCollideController : MonoBehaviour
{
    public static rewardCollideController instance;

    void Awake()
    {
        instance = this;
    }
    public int count;
    public int maxCount = 4;
    public TextMeshProUGUI text;
    public Slider slider;




    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("sliderpet").GetComponent<Slider>();
        text = GameObject.Find("sliderpet").transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        slider.maxValue = maxCount;
        text.text = count.ToString();
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("butterfly"))
        {
            //Debug.Log("reward");
            other.transform.parent.gameObject.SetActive(false);
            count++;
            text.text = count.ToString();
            slider.value = count;
            if (count == maxCount)
            {

                RewardController.instance.DisplayReward();

            }
        }
    }
}

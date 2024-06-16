using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;
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

    public JawapanPrasekolahController jawapan;

    // Start is called before the first frame update
    void Start()
    {
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();
        slider = GameObject.Find("sliderpet").GetComponent<Slider>();
        text = GameObject.Find("sliderpet").transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        Debug.Log(PlayerPrefs.GetInt(jawapan.sceneName + "_Reward"));

        StartCoroutine(lateStart());
    }

    IEnumerator lateStart()
    {
        yield return new WaitForSeconds(1f);
        slider.maxValue = maxCount;
        count = PlayerPrefs.GetInt(jawapan.sceneName + "_Reward");
        text.text = count.ToString();
        slider.value = count;
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
            PlayerPrefs.SetInt(jawapan.sceneName + "_Reward", count);
            if (!transform.GetChild(3).GetComponent<AudioSource>().isPlaying)
            {
                transform.GetChild(3).GetComponent<AudioSource>().Play();
            }
            if (count == maxCount)
            {
                count = 0;
                slider.value = count;
                PlayerPrefs.SetInt(jawapan.sceneName + "_Reward", count);
                RewardController.instance.DisplayReward();
            }
        }
    }
}

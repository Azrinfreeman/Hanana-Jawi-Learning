using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadRewardSliderValue2 : MonoBehaviour
{
    public Slider slider;
    public rewardCollideController2 rewardCollider;

    // Start is called before the first frame update
    void Start()
    {
        rewardCollider = GameObject.Find("rewardCollect").GetComponent<rewardCollideController2>();
        slider = GetComponent<Slider>();
        slider.maxValue = rewardCollider.maxCount;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = rewardCollider.slider.value;
    }
}

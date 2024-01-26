using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class loadRewardValue2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public rewardCollideController2 rewardCollider;
    // Start is called before the first frame update
    void Start()
    {
        rewardCollider = GameObject.Find("rewardCollect").GetComponent<rewardCollideController2>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = rewardCollider.count.ToString() + "/" + rewardCollider.maxCount;
    }
}

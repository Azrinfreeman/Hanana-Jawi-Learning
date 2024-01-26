using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class RewardController : MonoBehaviour
{
    public static RewardController instance;
    public JawapanPrasekolahController jawapan;

    void Awake()
    {
        instance = this;
    }

    public List<Transform> rewardItem;
    public List<Image> rewardAnimals;

    public Image rewardHintImage;
    public int a = 0;

    public bool isAtEnd;
    // Start is called before the first frame update
    void Start()
    {
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();
        rewardHintImage = GameObject.Find("rewardHint").GetComponent<Image>();
        for (int i = 0; i < transform.childCount; i++)
        {
            rewardItem.Add(transform.GetChild(i).GetComponent<Transform>());
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            rewardAnimals.Add(transform.GetChild(i).transform.GetChild(0).GetComponent<Image>());
        }

        StartCoroutine(CheckAndDisplayReward());
        ShowNextHint();

    }

    IEnumerator CheckAndDisplayReward()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < transform.childCount; i++)
        {

            if (PlayerPrefs.GetInt(jawapan.sceneName + "_" + i) == 1)
            {
                rewardItem[i].gameObject.SetActive(true);

            }
        }
    }

    IEnumerator ShowingHint()
    {
        yield return new WaitForSeconds(0.3f);
        //display next animal hint reward
        while (a < rewardItem.Count)
        {

            if (!rewardItem[a].gameObject.activeSelf)
            {

                rewardHintImage.sprite = rewardItem[a].GetChild(0).transform.GetComponent<Image>().sprite;
                a = rewardItem.Count;
                goto skipHint;
            }
            else
            {
                a++;
            }
        }
        a = 0;
        //check if no more hint
        while (a < rewardItem.Count)
        {

            if (rewardItem[a].gameObject.activeSelf)
            {

                a++;

            }
            if (a == rewardItem.Count)
            {
                rewardHintImage.gameObject.SetActive(false);
                isAtEnd = true;
                rewardCollideController.instance.count = rewardCollideController.instance.maxCount;
                rewardCollideController.instance.slider.value = rewardCollideController.instance.slider.maxValue;
            }
        }
    skipHint:

        a = 0;

    }

    void ShowNextHint()
    {
        StartCoroutine(ShowingHint());

    }

    IEnumerator processReward()
    {
        int i = 0;

        while (i < rewardItem.Count)
        {
            if (!rewardItem[i].gameObject.activeSelf)
            {
                if (PlayerPrefs.GetInt(jawapan.sceneName + "_" + i) == 0)
                {
                    rewardItem[i].gameObject.SetActive(true);
                    if (rewardItem[i].GetComponent<PatrolController>().isMammals)
                    {
                        rewardItem[i].transform.GetChild(1).transform.gameObject.SetActive(true);
                    }
                    PlayerPrefs.SetInt(jawapan.sceneName + "_" + i, 1);
                }

                i = rewardItem.Count;
            }
            else
            {
                i++;
            }
        }
        if (rewardHintImage.gameObject.activeSelf)
        {
            transform.parent.GetChild(0).gameObject.SetActive(true);

            yield return new WaitForSeconds(1.3f);
            if (!isAtEnd)
            {

                if (rewardCollideController.instance.count == rewardCollideController.instance.maxCount)
                {
                    rewardCollideController.instance.count = 0;
                    rewardCollideController.instance.slider.value = rewardCollideController.instance.count;
                }
            }
            else
            {
                rewardCollideController.instance.slider.value = rewardCollideController.instance.count;
            }

        }
        yield return new WaitForSeconds(0.3f);
        ShowNextHint();
        transform.parent.GetChild(0).gameObject.SetActive(false);
    }

    public void DisplayReward()
    {
        StartCoroutine(processReward());
    }

    // Update is called once per frame
    void Update() { }
}

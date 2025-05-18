using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardDisplay : MonoBehaviour
{
    public Transform List;

    public int rewardCount;
    public JawapanPrasekolahController jawapan;

    // Start is called before the first frame update
    void Start()
    {
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        rewardCount = RewardController.instance.rewardItem.Count;
        yield return new WaitForSeconds(0.3f);
        Transform clone;

        for (int i = 0; i < rewardCount; i++)
        {
            clone = Instantiate(List);
            clone.transform.SetParent(transform);
            clone.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            RectTransform rt = GetComponent<RectTransform>();
            if (rewardCount < 3)
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 30f);
            }
            else
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 60f);
            }
        }
        for (int i = 0; i < rewardCount; i++)
        {
            //gambar
            transform
                .GetChild(i)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Image>()
                .sprite = RewardController.instance.rewardAnimals[i].sprite;
            // nama
            transform
                .GetChild(i)
                .transform.GetChild(0)
                .transform.GetChild(1)
                .GetComponent<TextMeshProUGUI>()
                .text = "Reward " + (i + 1);
            //selesai / belum
            transform
                .GetChild(i)
                .transform.GetChild(0)
                .transform.GetChild(2)
                .transform.GetChild(0)
                .gameObject.SetActive(false);
            transform
                .GetChild(i)
                .transform.GetChild(0)
                .transform.GetChild(2)
                .transform.GetChild(1)
                .gameObject.SetActive(false);

            if (
                PlayerPrefs.GetInt(
                    jawapan.sceneName + "_" + i + "_" + PlayerPrefs.GetInt("CurrentPlayerNo_")
                ) == 1
            )
            {
                Debug.Log("fetch from each level reward prefs");
                transform
                    .GetChild(i)
                    .transform.GetChild(0)
                    .transform.GetChild(2)
                    .transform.GetChild(1)
                    .gameObject.SetActive(true);
            }
            else
            {
                transform
                    .GetChild(i)
                    .transform.GetChild(0)
                    .transform.GetChild(2)
                    .transform.GetChild(0)
                    .gameObject.SetActive(true);
            }
        }
    }

    public void FetchReward() { }

    // Update is called once per frame
    void Update() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public List<Transform> health;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            health.Add(transform.GetChild(i).GetChild(0).GetComponent<Transform>());
        }
    }

    public void LoseHealth()
    {
        StartCoroutine(LosingHealth());
    }

    IEnumerator LosingHealth()
    {
        yield return new WaitForSeconds(1.5f);
        if (!GameObject.Find("beep").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("beep").GetComponent<AudioSource>().Play();
        }
        health[health.Count - 1].parent.GetChild(1).gameObject.SetActive(true);
        Transform transform = health[health.Count - 1].parent.GetChild(0);
        health.RemoveAt(health.Count - 1);
        Destroy(transform.gameObject);
    }

    // Update is called once per frame
    void Update() { }
}

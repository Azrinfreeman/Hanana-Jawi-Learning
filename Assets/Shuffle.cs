using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    public Transform[] answers;

    public int[] numbers;
    // Start is called before the first frame update
    void Start()
    {
        numbers = new int[3];
        answers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            answers[i] = transform.GetChild(i);
        }


        numbers[0] = Random.Range(0, transform.childCount);

        numbers[1] = Random.Range(0, transform.childCount);

        numbers[2] = Random.Range(0, transform.childCount);
    //randomNumber = Random.Range(0, 3);
    tryagain:
        if (
            numbers[0] != numbers[1]
            && numbers[0] != numbers[2]
            && numbers[1] != numbers[0]
            && numbers[1] != numbers[2]
        )
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                answers[i].SetSiblingIndex(numbers[i]);

            }
        }
        else
        {
            Debug.Log("not showing");
            while (numbers[0] == numbers[1] || numbers[0] == numbers[2])
            {
                numbers[0] = Random.Range(0, transform.childCount);
                numbers[1] = Random.Range(0, transform.childCount);
            }
            while (numbers[1] == numbers[2])
            {
                numbers[2] = Random.Range(0, transform.childCount);
            }

            goto tryagain;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadStars : MonoBehaviour
{
    public Transform[] stars;
    public JawapanPrasekolahController jawapan;
    public bool isFailed;


    // Start is called before the first frame update
    void Start()
    {
        jawapan = GameObject.Find("jawapan").GetComponent<JawapanPrasekolahController>();
        stars = new Transform[3];

        for (int i = 0; i < 3; i++)
        {
            stars[i] = transform.GetChild(i).transform.GetChild(0).GetComponent<Transform>().transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isFailed)
        {

            //two stars
            if (jawapan.steps >= 1 && jawapan.steps <= (jawapan.bil / 2))
            {

                stars[0].gameObject.SetActive(true);
            }
            else if (jawapan.steps > (jawapan.bil / 2) && jawapan.steps < jawapan.bil)
            {

                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(true);
            }
            else if (jawapan.steps == jawapan.bil)
            {

                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(true);
                stars[2].gameObject.SetActive(true);

            }
            else
            {
                stars[0].gameObject.SetActive(false);
                stars[1].gameObject.SetActive(false);
                stars[2].gameObject.SetActive(false);

            }
        }
        else
        {
            //two stars
            if (jawapan.stepsSoalanBetul >= 1 && jawapan.stepsSoalanBetul <= 2)
            {

                stars[0].gameObject.SetActive(true);
            }
            else if (jawapan.stepsSoalanBetul == 3 || jawapan.stepsSoalanBetul == 4)
            {

                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(true);
            }
            else if (jawapan.stepsSoalanBetul == 5)
            {

                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(true);
                stars[2].gameObject.SetActive(true);

            }
            else
            {
                stars[0].gameObject.SetActive(false);
                stars[1].gameObject.SetActive(false);
                stars[2].gameObject.SetActive(false);

            }
        }
    }
}

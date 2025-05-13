using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorStart : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        startAnimation();
    }

    // Update is called once per frame
    void Update() { }

    public void startAnimation()
    {
        anim.Play("CloudsIdle", 0, 0.3f);
    }
}

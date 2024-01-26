using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DetailAnimationController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HideUi()
    {
        animator.SetBool("istrue", true);
    }

    public void ShowUi()
    {
        animator.SetBool("istrue", false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

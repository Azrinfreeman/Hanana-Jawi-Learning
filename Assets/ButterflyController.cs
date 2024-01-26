using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    public static ButterflyController instance;

    void Awake()
    {
        instance = this;
    }
    public List<Transform> butterfly;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            butterfly.Add(transform.GetChild(i).transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

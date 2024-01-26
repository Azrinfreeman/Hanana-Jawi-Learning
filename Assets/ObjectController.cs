using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    IEnumerator startChanging(Transform toObj)
    {
        yield return new WaitForSeconds(.6f);
        toObj.gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    public void changeObject(Transform toObj)
    {
        StartCoroutine(startChanging(toObj));
    }
}

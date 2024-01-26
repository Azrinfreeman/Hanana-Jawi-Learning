using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("boundary"))
        {
            //Debug.Log("reset position");
            transform.SetParent(GetComponent<DragController>().originalParent);
            transform.localPosition = GetComponent<DragController>().originalPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("slot"))
        {
            
            
        }
    }
}

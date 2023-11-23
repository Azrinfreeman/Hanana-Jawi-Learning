using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolController : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform[] moveSpots;
    private int randomSpot;

    public Vector2 oldPosition;
    public Vector2 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        oldPosition = transform.position;
    }

    void Flip()
    {
        if (oldPosition.x > newPosition.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = moveSpots[randomSpot].position;
        transform.position = Vector2.MoveTowards(
            transform.position,
            moveSpots[randomSpot].position,
            speed * Time.deltaTime
        );
        Flip();
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;

                oldPosition = newPosition;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}

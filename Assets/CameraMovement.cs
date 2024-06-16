using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep,
        minCamSize,
        maxCamSize;

    private Vector3 dragOrigin;
    public float zoomOutMin = 6;
    public float zoomOutMax = 24;

    [SerializeField]
    private SpriteRenderer mapRenderer;

    private float mapMinX,
        mapMaxX,
        mapMinY,
        mapMaxY;

    void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        PanCamera();
        //        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
    }

    void Zoom()
    {
        //PC
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ZoomIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ZoomOut();
        }

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomPinch(float increment)
    {
        /*
         float newSize;
         if (diff > 0)
         {
             newSize = cam.orthographicSize + diff;
         }
         else
         {
             newSize = cam.orthographicSize - diff;
         }
         cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

         cam.transform.position = ClampCamera(cam.transform.position);   //ograniczenie obszaru
         */

        Camera.main.orthographicSize = Mathf.Clamp(
            Camera.main.orthographicSize - increment,
            zoomOutMin,
            zoomOutMax
        );
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position); //ograniczenie obszaru
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        //
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        if (cam.orthographicSize > 36)
        {
            cam.orthographicSize = 38;
        }
        cam.transform.position = ClampCamera(cam.transform.position); //ograniczenie obszaru
    }

    private void PanCamera()
    {
        //save posotion of mouse in world space when drag

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        //for pinch zoom hand
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float diff = currentMagnitude - prevMagnitude;

            ZoomPinch(diff * 0.01f);
        }
        //calculate drag origin and new postiion
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            //print("origin " + dragOrigin + " new poistion " + cam.ScreenToWorldPoint(Input.mousePosition) + " =differe" + difference);
            //move the camera by distaance;
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}

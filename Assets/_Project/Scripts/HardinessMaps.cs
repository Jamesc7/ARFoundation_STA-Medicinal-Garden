using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardinessMaps : MonoBehaviour
{
    public float zoomOutMin = 1;
    public float zoomOutMax = 15;

    private void Awake()
    {
        var reference = transform.parent.Find("Screens").Find("MoreInfo").Find("Viewport").Find("Content").Find("HardinessMaps");
        for (int i = 0; i < reference.childCount; i++)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(reference.GetChild(i).gameObject.activeSelf);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            transform.GetChild(1).localPosition += new Vector3((touchZero.deltaPosition.x / transform.localScale.x), (touchZero.deltaPosition.y / transform.localScale.x), 0.0f);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            if((transform.localScale.x <= zoomOutMax) && (transform.GetChild(1).localScale.x >= zoomOutMin)) transform.localScale += new Vector3((difference * 0.01f), (difference * 0.01f), (difference * 0.01f));
            if (transform.localScale.x > zoomOutMax) transform.localScale = new Vector3(zoomOutMax, zoomOutMax, zoomOutMax);
            else if (transform.localScale.x < zoomOutMin) transform.localScale = new Vector3(zoomOutMin, zoomOutMin, zoomOutMin);
        }
    }

    public void Close()
    {
        transform.localScale = Vector3.one;
        transform.GetChild(1).localPosition = Vector3.zero;
        transform.gameObject.SetActive(false);
    }
}
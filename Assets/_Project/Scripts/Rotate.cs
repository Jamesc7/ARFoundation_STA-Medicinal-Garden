using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public float spinSpeed;
    [HideInInspector] public Canvas canvasUI;

    void Update()
    {
        if (!canvasUI.transform.Find("ToggleRotate").GetComponent<Toggle>().isOn)
        {
            //transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
            foreach (Transform child in transform.parent)
            {
                if(child != transform) child.transform.Rotate(0, (spinSpeed * Time.deltaTime), 0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCamera : MonoBehaviour
{
    public Camera ARCamera;
    public Camera SecondCamera;
    public GameObject button;
    public bool arEnabled = true;

    void Toggle()
    {
        arEnabled = !arEnabled;
        ARCamera.enabled = arEnabled;
        SecondCamera.enabled = !arEnabled;
    }

    void ButtonCastOff()
    {
        button.GetComponent<Button>().interactable = false;
    }

    void ButtonCastOn()
    {
        button.GetComponent<Button>().interactable = true;
    }

    void ButtonText()
    {
        button.GetComponent<GUIDisplay>().OnClick();
    }
}

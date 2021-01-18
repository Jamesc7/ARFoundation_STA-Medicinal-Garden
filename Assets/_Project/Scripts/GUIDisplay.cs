using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIDisplay : MonoBehaviour
{
    #region VARIABLES
    public GameObject connectedDisplay = null;
    public string Buttontext;
    public bool displayActive = false;
    public Color Button_color_normal = Color.white;
    public Color Button_color_active = Color.white;
    public int state = 0;
    #endregion // VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS
    void Start()
    {
        if (GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            Buttontext = GetComponentInChildren<TextMeshProUGUI>().text;
            if (Buttontext.Length >= 10) GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Baseline;
            else GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
        }
            if (TryGetComponent(out Image image)) image.color = Button_color_normal;
    }
    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    // If the button is clicked
    public void OnClick()
    {
        if (gameObject.name != "Options_Button")
        {
            Debug.Log("<color=red>Button Pressed</color>");
            switch (state)
            {
                case 0: // If the button is initially off
                    CleanUp();
                    GetComponentInChildren<Image>().color = Button_color_active;
                    GetComponentInChildren<TextMeshProUGUI>().text = Buttontext;
                    displayActive = false;
                    state = 1;
                    break;
                case 1:
                    GetComponentInChildren<TextMeshProUGUI>().text = "Close" + '\n';
                    GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
                    displayActive = true;
                    state = 2;
                    break;
                case 2:
                    GetComponentInChildren<TextMeshProUGUI>().text = Buttontext;
                    displayActive = false;
                    state = 1;
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("press");
            if(state == 0)
            {
                CleanUp();
                GetComponent<Image>().color = Button_color_active;
                GetComponentInChildren<TextMeshProUGUI>().text = "Close" + '\n';
                displayActive = true;
                state = 1;

            }
            else if (state == 1)
            {
                GetComponent<Image>().color = Button_color_normal;
                GetComponentInChildren<TextMeshProUGUI>().text = Buttontext;
                displayActive = false;
                state = 0;
            }
        }
    }
    public void Close()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = Buttontext; // Reset button text
        GetComponent<Image>().color = Button_color_normal; // Change the button color back to normal
        displayActive = false;
        state = 0;
    }
    public void CleanUp()
    {
        foreach(Transform child in transform.parent.parent.Find("MainPanel"))
        {
            if (child.GetComponent<GUIDisplay>() != null && child.GetComponent<GUIDisplay>().state != 0) child.GetComponent<GUIDisplay>().Close();
        }
    }
    #endregion // PUBLIC_METHODS
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableText : MonoBehaviour
{
    public string link = "";

    public void onClick()
    {
        if (link != "") Application.OpenURL(link);
    }
}

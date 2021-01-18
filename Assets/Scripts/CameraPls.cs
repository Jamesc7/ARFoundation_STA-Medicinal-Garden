using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class CameraPls : MonoBehaviour
{

    static WebCamTexture backCam;

    // Start is called before the first frame update
    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // The user authorized use of the microphone.
        }
        else
        {
            // Ask for permission or proceed without the functionality enabled.
            Permission.RequestUserPermission(Permission.Camera);
        }


        if (backCam == null)
            backCam = new WebCamTexture();

        GetComponent<Renderer>().material.mainTexture = backCam;

        if (!backCam.isPlaying)
            backCam.Play();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

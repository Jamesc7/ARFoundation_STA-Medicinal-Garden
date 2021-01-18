using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    #region VARIABLES
    public bool interactable = false;
    
    private readonly float maxScale = 2.0f;
    private readonly float minScale = 0.2f;
    private bool rendererOn = false;
    #endregion //VARIABLES

    #region UNITY_MONOBEHAVOIUR_METHODS
    // Update is called once per frame
    void Update()
    {
        if (!RendererCheck() && rendererOn) // If the mesh renderer was just turned off
        {
            // Reset rotation and scale
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            rendererOn = false;
        }
        else if (RendererCheck() && !rendererOn) rendererOn = true;

        if(interactable && Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (Vector3.Dot(transform.up, Vector3.up) >= 0) transform.Rotate(transform.up, -Vector3.Dot(touch.deltaPosition/10, Camera.main.transform.right), Space.World);
                        else transform.Rotate(transform.up, Vector3.Dot(touch.deltaPosition/10, Camera.main.transform.right), Space.World);
                        transform.Rotate(Camera.main.transform.right, Vector3.Dot(touch.deltaPosition/10, Camera.main.transform.up), Space.World);
                        break;
                    default: break;
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                var zoom = Vector3.Distance(touch0.position, touch1.position) / Vector3.Distance(touch0.position - touch0.deltaPosition, touch1.position - touch1.deltaPosition);
                if (zoom == 0 || zoom > 10) return;
                var scaleChange = (transform.localScale + (Vector3.LerpUnclamped(new Vector3(touch0.position.magnitude, touch0.position.magnitude, touch0.position.magnitude), transform.localScale, 1 / zoom)) / 200);
                if (scaleChange.x >= minScale && scaleChange.x <= maxScale) transform.localScale = scaleChange;
            }
        }
    }
    #endregion //UNITY_MONOBEHAVOIUR_METHODS
    #region PRIVATE_METHODS
    private bool RendererCheck()
    {
        var check = false;
        if (GetComponent<MeshRenderer>() != null)
        {
            if (GetComponent<MeshRenderer>().enabled) check = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<MeshRenderer>() != null)
                {
                    if (child.GetComponent<MeshRenderer>().enabled) check = true;
                }
            }
        }
        return check;
    }
    #endregion
}

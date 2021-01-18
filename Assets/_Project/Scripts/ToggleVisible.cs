using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum TYPES
{
    VISIBLESELECT = 0,
    TOGGLEBUTTON = 1
}
public class ToggleVisible : MonoBehaviour
{
    public TYPES type;
    public GameObject pointer = null;

    private bool state = true;

    #region PUBLIC_METHODS
    public void Open()
    {
        if(transform.Find("Background").gameObject.activeSelf) transform.Find("Background").gameObject.SetActive(false);
        if (!transform.Find("VisibleSelect").gameObject.activeSelf) transform.Find("VisibleSelect").gameObject.SetActive(true);
        if (!transform.Find("DisableButton").gameObject.activeSelf) transform.Find("DisableButton").gameObject.SetActive(true);
    }

    public void Close()
    {
        if (!transform.Find("Background").gameObject.activeSelf) transform.Find("Background").gameObject.SetActive(true);
        if (transform.Find("VisibleSelect").gameObject.activeSelf) transform.Find("VisibleSelect").gameObject.SetActive(false);
        if (transform.Find("DisableButton").gameObject.activeSelf) transform.Find("DisableButton").gameObject.SetActive(false);
        
    }

    public void Toggle()
    {
        state = !state;
        transform.GetChild(0).GetComponent<SwitchSprite>().switchSprite(state);
        if (pointer.TryGetComponent(out MeshRenderer rend) && rend.enabled != state) rend.enabled = state;
    }

    public void CleanUp()
    {
        state = true;
        if (type == TYPES.TOGGLEBUTTON)
        {
            transform.GetChild(0).GetComponent<SwitchSprite>().switchSprite(true);
            if (pointer != null && pointer.TryGetComponent(out MeshRenderer rend) && rend.enabled != true) rend.enabled = true;
        }
        else if (type == TYPES.VISIBLESELECT)
        {
            foreach (Transform child in transform.Find("VisibleSelect").Find("Viewport").Find("Content"))
            {
                if (child.TryGetComponent(out ToggleVisible comp)) comp.CleanUp();
            }
            Close();
        }
    }
    #endregion //PUBLIC_METHODS
}

#region DISPLAY_EDITOR
#if UNITY_EDITOR
[CustomEditor(typeof(ToggleVisible))]
[CanEditMultipleObjects]
public class ToggleVisibleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ToggleVisible _object = (ToggleVisible)target;
        _object.type = (TYPES)EditorGUILayout.EnumPopup("Script Type:", _object.type);
        EditorGUILayout.Space();
        switch (_object.type)
        {
            case (TYPES.VISIBLESELECT):
                break;
            case (TYPES.TOGGLEBUTTON):
                _object.pointer = (GameObject)EditorGUILayout.ObjectField("Pointer:", _object.pointer, typeof(GameObject), true);
                break;
            default: break;
        }
    }
}
#endif
#endregion //DISPLAY_EDITOR

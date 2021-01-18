using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSprite : MonoBehaviour
{
    public bool locked = true;
    public Image target;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    private void Update()
    {
        if (locked && target.sprite != lockedSprite) target.sprite = lockedSprite;
        else if (!locked && target.sprite != unlockedSprite)target.sprite = unlockedSprite;
    }
    public void switchSprite()
    {
        locked = !locked;
    }

    public void switchSprite(bool state)
    {
        locked = state;
    }

    public void cleanUp()
    {
        locked = true;
        GetComponent<Toggle>().isOn = true;
    }
}

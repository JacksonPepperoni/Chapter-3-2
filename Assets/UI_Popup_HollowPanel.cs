using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_HollowPanel : UI_Popup
{
    private void OnEnable()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        return true;

    }
}
